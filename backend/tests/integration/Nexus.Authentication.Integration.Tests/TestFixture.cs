using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Nexus.Authentication.Application.Abstractions.UnitOfWork;
using Nexus.Authentication.Infrastructure.Persistence.Contexts;
using Nexus.Authentication.Infrastructure.Persistence;
using WireMock.Server;
using Xunit;

namespace Nexus.Authentication.Integration.Tests;

public class TestFixture : IAsyncLifetime
{
    private readonly SqliteConnection _connection;
    private readonly DbContextOptions<AuthenticationContext> _options;
    public WireMockServer UserManagementServiceMock { get; } = WireMockServer.Start();

    public TestFixture()
    {
        _connection = new SqliteConnection("DataSource=:memory:");
        _connection.Open();

        _options = new DbContextOptionsBuilder<AuthenticationContext>().UseSqlite(_connection).Options;
    }

    public AuthenticationContext CreateDbContext() => new(_options);


    public IUnitOfWork CreateUnitOfWork(AuthenticationContext context)
    {
        return new UnitOfWork(context);
    }

    public async ValueTask InitializeAsync()
    {
        using var ctx = CreateDbContext();
        await ctx.Database.EnsureCreatedAsync();
    }

    public async ValueTask ResetDatabaseAsync()
    {
        using var ctx = CreateDbContext();
        await ctx.Database.ExecuteSqlRawAsync(
            "DELETE FROM AccessData;");
    }

    public async ValueTask DisposeAsync()
    {
        await _connection.DisposeAsync();
    }
}