using Crossdyne.Toolkit.Results;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Nexus.UserManagement.Application.Abstractions.UnitOfWork;
using Nexus.UserManagement.Application.Features.Users.Commands.ResetPasswordSendCode;
using Nexus.UserManagement.Domain.Models;
using Nexus.UserManagement.Domain.SmartEnums;
using Nexus.UserManagement.Domain.ValueObjects.User;
using Nexus.UserManagement.Infrastructure.Outbox;
using Nexus.UserManagement.Infrastructure.Persistence.Contexts;
using Nexus.UserManagement.Infrastructure.Persistence.Repositories.Users;
using Shared.Contracts.UserManagement.Events;
using Shared.Test.Cache;
using Xunit;

namespace Nexus.UserManagement.Integration.Tests.Handlers.Commands
{
    public class ResetPasswordSendCodeCommandHandlerTests : IClassFixture<TestFixture>, IAsyncLifetime
    {
        private readonly TestFixture _fixture;
        private readonly UserManagementContext _context;
        private readonly UserRepository _repo;
        private readonly IUnitOfWork _uow;
        private readonly OutboxSignal _outboxSignal;
        private readonly InMemoryCacheService _cache;
        private readonly ResetPasswordSendCodeCommandHandler _handler;
        private readonly CancellationToken _ct = default;

        public ResetPasswordSendCodeCommandHandlerTests(TestFixture fixture)
        {
            _fixture = fixture;

            _context = fixture.CreateDbContext();
            _repo = new UserRepository(_context);
            _outboxSignal = (OutboxSignal)fixture.CreateOutboxSignal();
            _cache = new InMemoryCacheService();
            _uow = fixture.CreateUnitOfWork(_context);

            _handler = new ResetPasswordSendCodeCommandHandler(_repo, _uow, _cache);
        }

        public async ValueTask InitializeAsync() => await _fixture.ResetDatabaseAsync();

        public async ValueTask DisposeAsync()
        {
            _outboxSignal.Dispose();
            await _context.DisposeAsync();
        }

        [Fact]
        public async Task Handle_ExistingUser_ShouldSaveCodeToCacheAndCreateOutboxMessage()
        {
            var uniqueFriendshipCode = $"RGNGJU-{Guid.NewGuid():N}".Substring(0, 11); 
            
            var login = Login.Create("testuser_2024");
            var user = User.Create(
                login,
                UserName.Create("Test User"),
                Email.Create("test@example.com"),
                FriendshipCode.Create(uniqueFriendshipCode),
                statusId: EnumStatus.Active.Id,
                genderId: null,
                countryId: null);

            await _context.Users.AddAsync(user, _ct);
            await _context.SaveChangesAsync(_ct);

            var command = new ResetPasswordSendCodeCommand(login.Value);

            var result = await _handler.Handle(command, _ct);

            result.IsSuccess.Should().BeTrue();

            var cachedCode = await _cache.GetStringAsync($"ConfirmCode for {login.Value.ToLowerInvariant()}");
            cachedCode.Should().NotBeNullOrEmpty();
            cachedCode!.Length.Should().Be(6);

            var outboxMessages = await _context.Set<OutboxMessage>()
                .AsNoTracking()
                .Where(m => m.EventType == typeof(PasswordResetRequestedIntegrationEvent).FullName)
                .ToListAsync(_ct);

            outboxMessages.Should().ContainSingle();
        }

        [Fact]
        public async Task Handle_NonExistingUser_ShouldReturnNotFound()
        {
            var command = new ResetPasswordSendCodeCommand("ghost_user");
            var result = await _handler.Handle(command, _ct);

            result.IsSuccess.Should().BeFalse();
            result.Errors.First().Code.Should().Be(ErrorCode.NotFound);

            var cachedCode = await _cache.GetStringAsync("ConfirmCode for ghost_user");
            cachedCode.Should().BeNull();
        }
    }
}