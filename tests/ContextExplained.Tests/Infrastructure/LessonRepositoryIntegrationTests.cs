using ContextExplained.Services.DTOs;
using ContextExplained.Core.Entities;
using ContextExplained.Core.ValueObjects;
using ContextExplained.Infrastructure.Adapters;
using ContextExplained.Infrastructure.Repositories;
using ContextExplained.Infrastructure.DataSeeder;
using Microsoft.EntityFrameworkCore;
using ContextExplained.Tests.Common;
using Microsoft.Data.Sqlite;

namespace ContextExplained.Tests.Infrastructure;
public abstract class RepositoryTestBase
{
    protected async Task<ApplicationDbContext> CreateContextAsync(Func<ApplicationDbContext, Task>? seeder = null)
    {
        var connection = new SqliteConnection("Filename=:memory");
        connection.Open();

        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseSqlite(connection)
            .Options;

        var context = new ApplicationDbContext(options);
        
        if (seeder != null)
        {
            await seeder(context);
        }

        return context;
    }
}
public class LessonRepositoryIntegrationTests : IClassFixture<SqliteDbFixture>
{
    private readonly ApplicationDbContext _dbContext;
    private readonly LessonRepository _repository;
    private readonly SqliteDbFixture _fixture;

    public LessonRepositoryIntegrationTests(SqliteDbFixture fixture)
    {
        _fixture = fixture;
        _dbContext = fixture.Context;
        _repository = new LessonRepository(_dbContext);
    }

    private async Task SeedLessonPath()
    {
        await _fixture.SeedAsync(LessonPathSeeder.SeedAsync);
    }
    [Fact]
    public async Task TestSeeding()
    {
        await _fixture.ClearDatabaseAsync();
        await SeedLessonPath();

        var lessonPaths = await _dbContext.LessonPaths.ToListAsync();
        Assert.NotEmpty(lessonPaths);
        
        var genesisPath = lessonPaths.FirstOrDefault(lp => lp.Book.Contains(BibleBook.Genesis.Name));

        Assert.Equal(BibleBook.Genesis.Name, genesisPath?.Book);
        Assert.Equal(1, genesisPath?.Sequence);
    }

    [Fact]
    public async Task GetAllLessonsAsync_ReturnsAllLessons()
    {
        await _fixture.ClearDatabaseAsync();
        await SeedLessonPath();

        var lessons = FakeLessonGenerator.CreateManyRandom(2).ToList();

        await _dbContext.Lessons.AddRangeAsync(lessons);
        await _dbContext.SaveChangesAsync();

        var getLessons = await _repository.GetAllLessonsAsync();

        Assert.Equal(2, getLessons.Count());
        Assert.Contains(getLessons, l => l.Passage == lessons[0].Passage);
        Assert.Contains(getLessons, l => l.Passage == lessons[1].Passage);
    }

    [Fact]
    public async Task GetAllLessonsAsync_ReturnsChronologically_WhenSameBookAndChapter()
    {
        await _fixture.ClearDatabaseAsync();
        await SeedLessonPath();

        var lesson1 = FakeLessonGenerator.Create(BibleBook.Genesis.Name, 1, new VerseRange(1, 5));
        var lesson2 = FakeLessonGenerator.Create(BibleBook.Genesis.Name, 1, new VerseRange(5, 10));

        await _dbContext.Lessons.AddRangeAsync(lesson1, lesson2);
        await _dbContext.SaveChangesAsync();

        var result = await _repository.GetAllLessonsAsync();
        foreach (var lesson in result)
        {
            Console.WriteLine($"Repo returning {lesson.BookChapterVerseRange()}");
        }
        var lessons = result.ToList();

        Assert.Equal(lesson2.BookChapterVerseRange(), lessons[0].BookChapterVerseRange());
        Assert.Equal(lesson1.BookChapterVerseRange(), lessons[1].BookChapterVerseRange());
    }

    //[Fact]
    //public async Task GetAllLessonAsync_ReturnsChronologicaly_WhenDifferentBook()
    //{
    //    var lesson1 = FakeLessonGenerator.CreateChronological(Books.1Peter);
    //}


    
    
}

