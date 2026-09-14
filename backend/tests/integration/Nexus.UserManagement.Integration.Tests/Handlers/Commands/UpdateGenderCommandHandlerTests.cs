using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Nexus.UserManagement.Application.Abstractions.UnitOfWork;
using Nexus.UserManagement.Application.Features.Genders.Commands.Update;
using Nexus.UserManagement.Domain.Models;
using Nexus.UserManagement.Infrastructure.Outbox;
using Nexus.UserManagement.Infrastructure.Persistence.Contexts;
using Nexus.UserManagement.Infrastructure.Persistence.Repositories.Genders;
using Xunit;

namespace Nexus.UserManagement.Integration.Tests.Handlers.Commands
{
    public class UpdateGenderCommandHandlerTests: IClassFixture<TestFixture>, IAsyncLifetime
    {
        private readonly TestFixture _fixture;
        private readonly UserManagementContext _context;
        private readonly GenderRepository _repo;
        private readonly IUnitOfWork _uow;
        private readonly OutboxSignal _outboxSignal;
        private readonly UpdateGenderCommandHandler _handler;
        private readonly CancellationToken _ct = default;

        public UpdateGenderCommandHandlerTests(TestFixture fixture)
        {
            _fixture = fixture;

            _context = fixture.CreateDbContext();
            _repo = new GenderRepository(_context);
            _outboxSignal = (OutboxSignal)fixture.CreateOutboxSignal();
            _uow = fixture.CreateUnitOfWork(_context);

            _handler = new UpdateGenderCommandHandler(_uow, _repo);
        }

        public async ValueTask InitializeAsync() => await _fixture.ResetDatabaseAsync();

        public async ValueTask DisposeAsync()
        {
            _outboxSignal.Dispose();
            await _context.DisposeAsync();
        }

        [Fact]
        public async Task Handle_ExistingId_ShouldUpdatedGender()
        {
            var gender = Gender.Create("Мужчина");
            await _context.Genders.AddAsync(gender, _ct);
            await _context.SaveChangesAsync(_ct);

            string newName = "NewName";

            var command = new UpdateGenderCommand(gender.Id, newName);

            var result = await _handler.Handle(command, _ct);

            result.IsSuccess.Should().BeTrue();
            var updated = await _context.Genders.FirstOrDefaultAsync(g => g.Id == gender.Id, _ct);
            updated?.Name.Value.Should().Be(newName);
        }

        [Fact]
        public async Task Handle_NonExistingId_ShouldReturnFailure()
        {
            var command = new UpdateGenderCommand(Guid.NewGuid(), "NewName");
            var result = await _handler.Handle(command, _ct);
            result.IsSuccess.Should().BeFalse();
        }
    
    }
}