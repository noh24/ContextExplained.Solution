using ContextExplained.Services.DTOs;
using ContextExplained.Core.Entities;
using ContextExplained.Core.ValueObjects;
using ContextExplained.Tests.Common;

namespace ContextExplained.IntegrationTests.Domain;

public class LessonTests
{
    [Fact]
    public void CreateChronological_ShouldReturnLesson_WhenDTOIsValid()
    {
        var lesson = FakeLessonGenerator.CreateRandom();

        Assert.Equal(lesson.Book, lesson.Book);
        Assert.Equal(lesson.Chapter, lesson.Chapter);
        Assert.Equal(lesson.VerseRange.ToString(), lesson.VerseRange.ToString());
        Assert.Equal(lesson.Passage, lesson.Passage);
        Assert.Equal(lesson.Context, lesson.Context);
        Assert.Equal(lesson.Themes, lesson.Themes);
        Assert.Equal(lesson.Reflection, lesson.Reflection);
        Assert.Equal(LessonPathType.Chronological.ToString(), lesson.PathType.ToString());
        Assert.True(lesson.CreatedAt <= DateTime.UtcNow);
    }

    [Fact]
    public void BooKChapterVerseRange_ShouldReturnCorrectString()
    {
        var lesson = FakeLessonGenerator.Create(book: "Genesis", chapter: 1, verseRange: new VerseRange(1, 5));
        Assert.Equal("Genesis 1:1-5", lesson.BookChapterVerseRange());

    }
}
