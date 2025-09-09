using ContextExplained.Infrastructure.Adapters;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace ContextExplained.Tests.Infrastructure;

public abstract class RepositoryTestBase
{
    protected ApplicationDbContext DbContext { get; private set; }
    protected async void CreateContextAsync()
    {
        var connection = new SqliteConnection("Filename=:memory:");
        await connection.OpenAsync();

        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseSqlite(connection)
            .Options;

        var dbContext = new ApplicationDbContext(options);
        await dbContext.Database.EnsureCreatedAsync();
        DbContext = dbContext;
    }
    protected async void SeedDataAsync(Func<ApplicationDbContext, Task> seeder)
    {
        await seeder(DbContext);
    }
}