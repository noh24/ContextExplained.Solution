using ContextExplained.Core.DTOs;
using ContextExplained.Core.ValueObjects;

namespace ContextExplained.Tests.Common;

public static class FakeLessonDTOGenerator
{
    private static readonly Random _rand = new();

    public static LessonDTO Create(
        string? book = null,
        int? chapter = null,
        VerseRange? verseRange = null,
        string passage = "passage",
        string context = "context",
        string themes = "themes",
        string reflection = "reflection")
    {
        var canonicalBooks = LessonPaths.CanonicalOrder.Keys.ToList();
        var selectedBook = book ?? canonicalBooks[_rand.Next(canonicalBooks.Count)];

        var selectedChapter = chapter ?? _rand.Next(1, 51);
        var selectedVerseRange = verseRange ?? new VerseRange(_rand.Next(1, 10), _rand.Next(11, 51));

        return new LessonDTO
        {
            Book = selectedBook,
            Chapter = selectedChapter,
            VerseRange = selectedVerseRange,
            Passage = $"{passage} {selectedVerseRange}",
            Context = context,
            Themes = themes,
            Reflection = reflection
        };
    }

    public static IEnumerable<LessonDTO> CreateMany(int count)
    {
        return Enumerable.Range(0, count).Select(_ => Create()).ToList();
    }
}
