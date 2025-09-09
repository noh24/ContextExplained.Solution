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

public class LessonRepositoryIntegrationTests : RepositoryTestBase
{
    private readonly LessonRepository _repository;

    public LessonRepositoryIntegrationTests()
    {
        CreateContextAsync();
        _repository = new LessonRepository(DbContext);
    }

    private void SeedLessonPath()
    {
        SeedDataAsync(LessonPathSeeder.SeedAsync);
    }
    [Fact]
    public async Task TestSeeding()
    {
        SeedLessonPath();

        var lessonPaths = await DbContext.LessonPaths.ToListAsync();

        Assert.NotEmpty(lessonPaths);
        
        var genesisPath = lessonPaths.FirstOrDefault(lp => lp.Book.Contains(BibleBook.Genesis.Name));

        Assert.Equal(BibleBook.Genesis.Name, genesisPath?.Book);
        Assert.Equal(1, genesisPath?.Sequence);
    }

    [Fact]
    public async Task GetAllLessonsAsync_ReturnsAllLessons()
    {
        SeedLessonPath();

        var lessons = FakeLessonGenerator.CreateManyRandom(2).ToList();

        await DbContext.Lessons.AddRangeAsync(lessons);
        await DbContext.SaveChangesAsync();

        var getLessons = await _repository.GetAllLessonsAsync();

        Assert.Equal(2, getLessons.Count());
        Assert.Contains(getLessons, l => l.Passage == lessons[0].Passage);
        Assert.Contains(getLessons, l => l.Passage == lessons[1].Passage);
    }

    [Fact]
    public async Task GetAllLessonsAsync_ReturnsChronologically_WhenSameBookAndChapter()
    {
        SeedLessonPath();

        var lesson1 = FakeLessonGenerator.Create(BibleBook.Genesis, 1, new VerseRange(1, 5));
        var lesson2 = FakeLessonGenerator.Create(BibleBook.Genesis, 1, new VerseRange(5, 10));

        await DbContext.Lessons.AddRangeAsync(lesson1, lesson2);
        await DbContext.SaveChangesAsync();

        var result = await _repository.GetAllLessonsAsync();
        var lessons = result.ToList();

        Assert.Equal(2, lessons.Count);
        Assert.Equal(lesson2.BookChapterVerseRange, lessons[0].BookChapterVerseRange);
        Assert.Equal(lesson1.BookChapterVerseRange, lessons[1].BookChapterVerseRange);
    }

    [Fact]
    public async Task GetAllLessonAsync_ReturnsChronologicaly_WhenDifferentBook()
    {
        SeedLessonPath();

        var exodus = FakeLessonGenerator.Create(BibleBook.Exodus, 1, new VerseRange(15,30));
        var genesis = FakeLessonGenerator.Create(BibleBook.Genesis, 2, new VerseRange(15,30));
        var numbers = FakeLessonGenerator.Create(BibleBook.Numbers, 15, new VerseRange(1, 30));

        await DbContext.AddRangeAsync(exodus, genesis, numbers);
        await DbContext.SaveChangesAsync();

        var result = await _repository.GetAllLessonsAsync();
        var lessons = result.ToList();

        Assert.Equal(3, lessons.Count);
        Assert.Equal(numbers.BookChapterVerseRange, lessons[0].BookChapterVerseRange);
        Assert.Equal(exodus.BookChapterVerseRange,lessons[1].BookChapterVerseRange);
        Assert.Equal(genesis.BookChapterVerseRange, lessons[2].BookChapterVerseRange);
    }

    [Fact]
    public async Task GetAllLessonsAsync_ShouldThrow_WhenNoLessonPath()
    {
        var fakeLessons = FakeLessonGenerator.CreateManyRandom(2);

        await DbContext.AddRangeAsync(fakeLessons);
        await DbContext.SaveChangesAsync();

        await Assert.ThrowsAsync<InvalidOperationException>(_repository.GetAllLessonsAsync);
    }

    [Fact]
    public async Task GetPreviousLessonAsync_ReturnMostRecentLesson_WhenSameBookAndChapter()
    {
        SeedLessonPath();

        var genesis = FakeLessonGenerator.Create(BibleBook.Genesis, 1, new VerseRange(1, 5));
        var genesis2 = FakeLessonGenerator.Create(BibleBook.Genesis, 1, new VerseRange(10, 15));
        //var numbers = FakeLessonGenerator.Create(BibleBook.Numbers, 5, new VerseRange(20, 30));

        await DbContext.AddRangeAsync(genesis, genesis2);
        await DbContext.SaveChangesAsync();

        var result = await _repository.GetPreviousLessonAsync();

        Assert.Equal(genesis2.BookChapterVerseRange, result?.BookChapterVerseRange);
    }

}

