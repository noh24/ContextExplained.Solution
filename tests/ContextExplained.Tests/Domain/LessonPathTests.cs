using ContextExplained.Core.Entities;
using ContextExplained.Core.ValueObjects;

namespace ContextExplained.Tests.Domain;

public class LessonPathTests
{
    [Fact]
    public void LessonPath_ShouldReturnLessonPath_WhenParamIsValid()
    {
        var lessonPath = new LessonPath(LessonPathType.Chronological, "Genesis", 1);

        Assert.Equal(LessonPathType.Chronological, lessonPath.PathType);
        Assert.Equal("Genesis", lessonPath.Book);
        Assert.Equal(1, lessonPath.Sequence);
    }
}
