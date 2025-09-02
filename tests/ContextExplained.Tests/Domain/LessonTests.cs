using ContextExplained.Core.DTOs;
using ContextExplained.Core.Entities;
using ContextExplained.Core.ValueObjects;
using ContextExplained.Tests.Common;

namespace ContextExplained.Tests.Domain;

public class LessonTests
{
    [Fact]
    public void Create_ShouldReturnLesson_WhenDTOIsValid()
    {
        var dto = FakeLessonDTOGenerator.Create();
        var lesson = Lesson.Create(dto);

        Assert.Equal(dto.Book, lesson.Book);
        Assert.Equal(dto.Chapter, lesson.Chapter);
        Assert.Equal(dto.VerseRange.ToString(), lesson.VerseRange.ToString());
        Assert.Equal(dto.Passage, lesson.Passage);
        Assert.Equal(dto.Context, lesson.Context);
        Assert.Equal(dto.Themes, lesson.Themes);
        Assert.Equal(dto.Reflection, lesson.Reflection);
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
        var dto = FakeLessonDTOGenerator.Create(book: "Genesis", chapter: 1, verseRange: new VerseRange(1, 5));
        var lesson = Lesson.Create(dto);
        Assert.Equal("Genesis 1:1-5", lesson.BookChapterVerseRange());

    }
}
