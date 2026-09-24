using System.Reflection;
using AutoMapper;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using Nexus.UserManagement.Application.Abstractions.Outbox;
using Nexus.UserManagement.Application.Abstractions.UnitOfWork;
using Nexus.UserManagement.Application.Mapping.Events;
using Nexus.UserManagement.Domain.Events;
using Nexus.UserManagement.Domain.Models;
using Nexus.UserManagement.Domain.SmartEnums;
using Nexus.UserManagement.Domain.ValueObjects.Role;
using Nexus.UserManagement.Domain.ValueObjects.Status;
using Nexus.UserManagement.Infrastructure.Outbox;
using Nexus.UserManagement.Infrastructure.Persistence.Contexts;
using Nexus.UserManagement.Integration.Tests.Contexts;
using Nexus.UserManagement.Infrastructure.Persistence;
using Shared.Contracts.UserManagement.Events;
using WireMock.Server;
using Xunit;

namespace Nexus.UserManagement.Integration.Tests;

public class TestFixture : IAsyncLifetime
{
    private readonly SqliteConnection _connection;
    private readonly DbContextOptions<UserManagementContext> _options;
    private readonly IMapper _mapper;
    private readonly EventTypeMappingRegistry _mappingRegistry;
    public WireMockServer FileServiceMock { get; } = WireMockServer.Start();

    public TestFixture()
    {
        _connection = new SqliteConnection("DataSource=:memory:");
        _connection.Open();

        _options = new DbContextOptionsBuilder<UserManagementContext>().UseSqlite(_connection).Options;

        _mapper = new MapperConfiguration(cfg => cfg.AddMaps(typeof(ChangeEmailRequestedEventMapper).Assembly), NullLoggerFactory.Instance).CreateMapper();

        _mappingRegistry = new EventTypeMappingRegistry();
        _mappingRegistry.Map<ChangeEmailRequestedDomainEvent, ChangeEmailRequestedIntegrationEvent>();
        _mappingRegistry.Map<PasswordResetRequestedDomainEvent, PasswordResetRequestedIntegrationEvent>();
        _mappingRegistry.Map<UserAccountDeletedDomainEvent, UserAccountDeletedIntegrationEvent>();
        _mappingRegistry.Map<UserPasswordResetDomainEvent, UserPasswordResetIntegrationEvent>();
        _mappingRegistry.Map<UserCreatedDomainEvent, UserCreatedIntegrationEvent>();
    }

    public TestUserManagementContext CreateDbContext() => new(_options);

    public IDbContextOutbox CreateOutbox(UserManagementContext context) =>
        new DbContextOutbox(context, _mapper, _mappingRegistry);

    public IOutboxSignal CreateOutboxSignal() => new OutboxSignal();

    public IUnitOfWork CreateUnitOfWork(UserManagementContext context)
    {
        var outbox = CreateOutbox(context);
        var signal = CreateOutboxSignal();
        return new UnitOfWork(context, outbox, signal);
    }

    public async ValueTask InitializeAsync()
    {
        using var ctx = CreateDbContext();
        await ctx.Database.EnsureCreatedAsync();
    
        var role = (Role)Activator.CreateInstance(
            typeof(Role),
            BindingFlags.Instance | BindingFlags.NonPublic,
            null,
            [RoleId.From(EnumRole.User.Id), RoleName.Create(EnumRole.User.Name)],
            null)!;

        var status = (Status)Activator.CreateInstance(
            typeof(Status),
            BindingFlags.Instance | BindingFlags.NonPublic,
            null,
            [EnumStatus.Active.Id, StatusName.Create(EnumStatus.Active.Name)],
            null)!;

        ctx.Roles.Add(role);
        ctx.Statuses.Add(status);

         await ctx.SaveChangesAsync();
    }

    public async ValueTask ResetDatabaseAsync()
    {
        using var ctx = CreateDbContext();
        await ctx.Database.ExecuteSqlRawAsync(
            "DELETE FROM outbox_messages;" +
            "DELETE FROM countries;" +
            "DELETE FROM users;");
    }

    public async ValueTask DisposeAsync()
    {
        await _connection.DisposeAsync();
    }
}