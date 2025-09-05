using ContextExplained.Core.ValueObjects;

namespace ContextExplained.Tests.Domain;

public class LessonPathTypeTests
{
    [Fact]
    public void Chronological_ShouldReturnLessonPathTypeChronological()
    {
        var pathType = LessonPathType.Chronological;

        Assert.Equal("Chronological", pathType.ToString());
    }
}
