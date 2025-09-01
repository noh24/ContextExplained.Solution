using ContextExplained.Core.ValueObjects;

namespace ContextExplained.Tests.Domain;

public class VerseRangeTests
{
    [Fact]
    public void Constructor_ShouldThrow_WhenStartGreaterThanEnd()
    {
        Assert.Throws<ArgumentNullException>(() => new VerseRange(10, 5));
    }

    [Theory]
    [InlineData(5, 5, "5")]
    [InlineData(1, 5, "1-5")]
    public void ToString_ShouldReturnCorrectFormat(int start, int end, string expected)
    {
        var range = new VerseRange(start, end);
        Assert.Equal(expected, range.ToString());
    }
}
