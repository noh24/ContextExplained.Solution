using ContextExplained.Core.ValueObjects;
using ContextExplained.Core.Entities;

namespace ContextExplained.Tests.Common;

public static class FakeLessonGenerator
{
    private static readonly Random _rand = new();

    public static Lesson Create(string book, int chapter, VerseRange verseRange)
    {
        var passage = $"{book} {chapter} {verseRange}";
        Console.WriteLine($"Entered Lesson {book} {chapter} {verseRange}");
        var builder = new LessonBuilder(book, chapter, verseRange, passage, "context", "themes", "reflection");
        Console.WriteLine($"LessonBuilder  {builder.Book} {builder.Chapter} {builder.VerseRange}");
        var lesson = Lesson.CreateChronological(builder);
        Console.WriteLine($"New Lesson {lesson.Book} {lesson.Chapter} {lesson.VerseRange}");

        return lesson;
    }

    public static Lesson CreateRandom()
    {
        var books = LessonPathsArchive.Chronological.Keys.ToList();
        var randomBook = books[_rand.Next(books.Count)].Name;

        var randomChapter = _rand.Next(1, 51);
        var randomVerseRange = new VerseRange(_rand.Next(1, 10), _rand.Next(11, 51));
        var passage = $"{randomBook} {randomChapter} {randomVerseRange}";
        var builder = new LessonBuilder(randomBook, randomChapter, randomVerseRange, passage, "context", "themes", "reflection");

        return Lesson.CreateChronological(builder);
    }

    public static IEnumerable<Lesson> CreateManyRandom(int count)
    {
        return Enumerable.Range(0, count).Select(_ => CreateRandom()).ToList();
    }
}
