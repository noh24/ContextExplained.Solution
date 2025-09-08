using ContextExplained.Infrastructure.Adapters;
using ContextExplained.Infrastructure.DataSeeder;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ContextExplained.IntegrationTests;

public class SqliteDbFixture : IAsyncLifetime
{
    public ApplicationDbContext Context { get; private set; }
    private readonly SqliteConnection _connection;

    public SqliteDbFixture()
    {
        _connection = new SqliteConnection("Filename=:memory:");
        _connection.Open();

        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseSqlite(_connection)
            .Options;

        Context = new ApplicationDbContext(options);
        Context.Database.OpenConnection();
        Context.Database.EnsureCreated();
    }
    public Task InitializeAsync() => Task.CompletedTask;

    public async Task SeedAsync(Func<ApplicationDbContext, Task> seeder)
    {
        await seeder(Context);
    }

    public async Task DisposeAsync()
    {
        await Context.DisposeAsync();
    }
}
