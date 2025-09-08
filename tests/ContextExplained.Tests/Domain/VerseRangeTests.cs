using ContextExplained.Core.ValueObjects;

namespace ContextExplained.IntegrationTests.Domain;

public class VerseRangeTests
{
    [Fact]
    public void Constructor_ShouldThrow_WhenStartGreaterThanEnd()
    {
        Assert.Throws<ArgumentException>(() => new VerseRange(10, 5));
    }

    [Theory]
    [InlineData(5, 5, "5")]
    [InlineData(1, 5, "1-5")]
    public void ToString_ShouldReturnCorrectFormat(int start, int end, string expected)
    {
        var range = new VerseRange(start, end);
        Assert.Equal(expected, range.ToString());
    }

    public static IEnumerable<object[]> VerseRangeTestData =>
        new List<object[]>
        {
            new object[] {"1-5", new VerseRange(1,5)},
            new object[] {"5", new VerseRange(5,5)}
        };
    [Theory]
    [MemberData(nameof(VerseRangeTestData))]
    public void FromString_ShouldReturnNewVerseRange(string verseRange, VerseRange expected)
    {
        Assert.Equal(expected, VerseRange.FromString(verseRange));
    }
}
