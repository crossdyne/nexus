using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Nexus.UserManagement.Application.Abstractions.UnitOfWork;
using Nexus.UserManagement.Application.Features.Genders.Commands.Delete;
using Nexus.UserManagement.Domain.Models;
using Nexus.UserManagement.Infrastructure.Outbox;
using Nexus.UserManagement.Infrastructure.Persistence.Contexts;
using Nexus.UserManagement.Infrastructure.Persistence.Repositories.Genders;
using Xunit;

namespace Nexus.UserManagement.Integration.Tests.Handlers.Commands
{
    public class DeleteGenderCommandHandlerTests : IClassFixture<TestFixture>, IAsyncLifetime
{
    private readonly TestFixture _fixture;
    private readonly UserManagementContext _context;
    private readonly GenderRepository _repo;
    private readonly IUnitOfWork _uow;
    private readonly OutboxSignal _outboxSignal;
    private readonly DeleteGenderCommandHandler _handler;
    private readonly CancellationToken _ct = default;

    public DeleteGenderCommandHandlerTests(TestFixture fixture)
    {
        _fixture = fixture;

        _context = fixture.CreateDbContext();
        _repo = new GenderRepository(_context);
        _outboxSignal = (OutboxSignal)fixture.CreateOutboxSignal();
        _uow = fixture.CreateUnitOfWork(_context);

        _handler = new DeleteGenderCommandHandler(_uow, _repo);
    }

    public async ValueTask InitializeAsync() => await _fixture.ResetDatabaseAsync();

    public async ValueTask DisposeAsync()
    {
        _outboxSignal.Dispose();
        await _context.DisposeAsync();
    }

    [Fact]
    public async Task Handle_ExistingId_ShouldDeleteGender_And_ReturnSuccess()
    {
        var gender = Gender.Create("Женщина");
        await _context.Genders.AddAsync(gender, _ct);
        await _context.SaveChangesAsync(_ct);

        var command = new DeleteGenderCommand(gender.Id);
        var result = await _handler.Handle(command, _ct);

        result.IsSuccess.Should().BeTrue();

        var deleted = await _context.Countries.AsNoTracking().FirstOrDefaultAsync(g => g.Id == gender.Id, _ct);
        deleted.Should().BeNull();
    }

    [Fact]
    public async Task Handle_NonExistingId_ShouldReturnFailure()
    {
        var command = new DeleteGenderCommand(Guid.NewGuid());
        var result = await _handler.Handle(command, _ct);

        result.IsSuccess.Should().BeFalse();
    }
    }
}