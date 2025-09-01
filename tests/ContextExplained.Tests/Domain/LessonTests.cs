using ContextExplained.Core.DTOs;
using ContextExplained.Core.Entities;
using ContextExplained.Core.ValueObjects;

namespace ContextExplained.Tests.Domain;

public class LessonTests
{
    private readonly LessonDTO _dto = new LessonDTO
    {
        Book = "John",
        Chapter = 3,
        VerseRange = new VerseRange(16, 17),
        Passage = "For God so loved the world...",
        Context = "Context text",
        Themes = "Love, Faith",
        Reflection = "Reflection text"
    };

    [Fact]
    public void Create_ShouldReturnLesson_WhenDTOIsValid()
    {
        var lesson = Lesson.Create(_dto);

        Assert.Equal("John", lesson.Book);
        Assert.Equal(3, lesson.Chapter);
        Assert.Equal("16-17", lesson.VerseRange.ToString());
        Assert.Equal("For God so loved the world...", lesson.Passage);
        Assert.Equal("Context text", lesson.Context);
        Assert.Equal("Love, Faith", lesson.Themes);
        Assert.Equal("Reflection text", lesson.Reflection);
        Assert.True(lesson.CreatedAt <= DateTime.UtcNow);
    }

    [Fact]
    public void Create_ShouldThrow_WhenDtoIsNull()
    {
        LessonDTO dto = null!;
        Assert.Throws<ArgumentNullException>(() => Lesson.Create(dto));
    }

    [Fact]
    public void BooKChapterVerseRange_ShouldReturnCorrectString()
    {
        var lesson = Lesson.Create(_dto);
        Assert.Equal("John 3:16-17", lesson.BookChapterVerseRange());

    }
}
