using ContextExplained.Services.DTOs;
using ContextExplained.Core.Entities;
using ContextExplained.Core.ValueObjects;
using ContextExplained.Infrastructure.Adapters;
using ContextExplained.Infrastructure.Repositories;
using ContextExplained.Infrastructure.DataSeeder;
using Microsoft.EntityFrameworkCore;
using ContextExplained.Tests.Common;

namespace ContextExplained.Tests.Infrastructure;

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
        await SeedLessonPath();

        var lessonPaths = await _dbContext.LessonPaths.ToListAsync();
        Assert.NotEmpty(lessonPaths);
        
        var genesisPath = lessonPaths.FirstOrDefault(lp => lp.Book.Contains("Genesis"));

        Assert.Equal("Genesis", genesisPath?.Book);
        Assert.Equal(1, genesisPath?.Sequence);
    }

    [Fact]
    public async Task GetAllLessonsAsync_ReturnsAllLessons()
    {
        var lessons = FakeLessonGenerator.CreateMany(2).ToList();

        await _dbContext.Lessons.AddRangeAsync(lessons);
        await _dbContext.SaveChangesAsync();
        await SeedLessonPath();

        var getLessons = await _repository.GetAllLessonsAsync();

        Assert.Equal(2, getLessons.Count());
        Assert.Contains(getLessons, l => l.Passage == lessons[0].Passage);
        Assert.Contains(getLessons, l => l.Passage == lessons[1].Passage);
    }

    [Fact]
    public async Task GetAllLessonsAsync_ReturnsChronologically_WhenSameBookAndChapter()
    {
        var lesson1 = FakeLessonGenerator.CreateChronological("Genesis", 1, new VerseRange(1, 5));
        var lesson2 = FakeLessonGenerator.CreateChronological("Genesis", 1, new VerseRange(5, 10));

        await _dbContext.Lessons.AddRangeAsync(lesson1, lesson2);
        await _dbContext.SaveChangesAsync();
        await SeedLessonPath();

        var result = await _repository.GetAllLessonsAsync();
        var lessons = result.ToList();
        Assert.Equal(lesson2.BookChapterVerseRange(), lessons[0].BookChapterVerseRange());
        Assert.Equal(lesson1.BookChapterVerseRange(), lessons[1].BookChapterVerseRange());
    }


}
