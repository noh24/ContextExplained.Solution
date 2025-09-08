using ContextExplained.Core.ValueObjects;
using ContextExplained.Core.Entities;

namespace ContextExplained.Tests.Common;

public static class FakeLessonGenerator
{
    private static readonly Random _rand = new();

    public static Lesson CreateChronological(
        string? book = null,
        int? chapter = null,
        VerseRange? verseRange = null,
        string passage = "passage",
        string context = "context",
        string themes = "themes",
        string reflection = "reflection")
    {
        var books = LessonPathsArchive.Chronological.Keys.ToList();
        var selectedBook = book ?? books[_rand.Next(books.Count)];

        var selectedChapter = chapter ?? _rand.Next(1, 51);
        var selectedVerseRange = verseRange ?? new VerseRange(_rand.Next(1, 10), _rand.Next(11, 51));
        var selectedPassage = $"{passage} {selectedBook} {selectedChapter} {selectedVerseRange}";

        var builder = new LessonBuilder(selectedBook, selectedChapter, selectedVerseRange, selectedPassage, context, themes, reflection);

        return Lesson.CreateChronological(builder);
    }

    public static IEnumerable<Lesson> CreateMany(int count)
    {
        return Enumerable.Range(0, count).Select(_ => CreateChronological()).ToList();
    }
}
