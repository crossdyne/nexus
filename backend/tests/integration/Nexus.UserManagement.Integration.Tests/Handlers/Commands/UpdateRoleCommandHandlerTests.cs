using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Nexus.UserManagement.Application.Abstractions.UnitOfWork;
using Nexus.UserManagement.Application.Features.Roles.Commands.Update;
using Nexus.UserManagement.Domain.Models;
using Nexus.UserManagement.Infrastructure.Outbox;
using Nexus.UserManagement.Infrastructure.Persistence.Contexts;
using Nexus.UserManagement.Infrastructure.Persistence.Repositories.Roles;
using Xunit;

namespace Nexus.UserManagement.Integration.Tests.Handlers.Commands
{
    public class UpdateRoleCommandHandlerTests: IClassFixture<TestFixture>, IAsyncLifetime
    {
        private readonly TestFixture _fixture;
        private readonly UserManagementContext _context;
        private readonly RoleRepository _repo;
        private readonly IUnitOfWork _uow;
        private readonly OutboxSignal _outboxSignal;
        private readonly UpdateRoleCommandHandler _handler;
        private readonly CancellationToken _ct = default;

        public UpdateRoleCommandHandlerTests(TestFixture fixture)
        {
            _fixture = fixture;

            _context = fixture.CreateDbContext();
            _repo = new RoleRepository(_context);
            _outboxSignal = (OutboxSignal)fixture.CreateOutboxSignal();
            _uow = fixture.CreateUnitOfWork(_context);

            _handler = new UpdateRoleCommandHandler(_uow, _repo);
        }

        public async ValueTask InitializeAsync() => await _fixture.ResetDatabaseAsync();

        public async ValueTask DisposeAsync()
        {
            _outboxSignal.Dispose();
            await _context.DisposeAsync();
        }

        [Fact]
        public async Task Handle_ExistingId_ShouldUpdatedRole()
        {
            var role = Role.Create("Админ");
            await _context.Roles.AddAsync(role, _ct);
            await _context.SaveChangesAsync(_ct);

            string newName = "NewName";

            var command = new UpdateRoleCommand(role.Id, newName);

            var result = await _handler.Handle(command, _ct);

            result.IsSuccess.Should().BeTrue();
            var updated = await _context.Roles.FirstOrDefaultAsync(r => r.Id == role.Id, _ct);
            updated?.Name.Value.Should().Be(newName);
        }

        [Fact]
        public async Task Handle_NonExistingId_ShouldReturnFailure()
        {
            var command = new UpdateRoleCommand(Guid.NewGuid(), "NewName");
            var result = await _handler.Handle(command, _ct);
            result.IsSuccess.Should().BeFalse();
        }
    }
}