using ContextExplained.Infrastructure.Adapters;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace ContextExplained.IntegrationTests;

public class SqliteDbFixture : IDisposable
{
    public ApplicationDbContext Context { get; }
    private readonly SqliteConnection _connection;

    public SqliteDbFixture()
    {
        _connection = new SqliteConnection("Filename=:memory:");
        _connection.Open();

        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseSqlite(_connection)
            .Options;

        Context = new ApplicationDbContext(options);
        Context.Database.EnsureCreated();
    }

    public void Dispose()
    {
        Context.Dispose();
        _connection.Dispose();
    }
}
