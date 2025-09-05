using ContextExplained.Core.ValueObjects;
using System.Runtime.ConstrainedExecution;

namespace ContextExplained.Core.Entities;
public class Lesson
{
    public Guid Id { get; private set; }
    public string Book { get; private set; } = null!;
    public int Chapter { get; private set; }
    public VerseRange VerseRange { get; private set; }
    public string Passage { get; private set; } = null!;
    public string Context { get; private set; } = null!;
    public string Themes { get; private set; } = null!;
    public string Reflection { get; private set; } = null!;
    public LessonPathType PathType { get; private set; }
    public DateTime CreatedAt { get; private set; }

    private Lesson() { } // EF Core
    private Lesson(string book, int chapter, VerseRange verseRange, string passage, string context, string themes, string reflection, LessonPathType pathType)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(book);
        if (chapter <= 0) throw new ArgumentException(null, nameof(chapter));
        ArgumentNullException.ThrowIfNull(verseRange);
        ArgumentException.ThrowIfNullOrWhiteSpace(passage);
        ArgumentException.ThrowIfNullOrWhiteSpace(context);
        ArgumentException.ThrowIfNullOrWhiteSpace(themes);
        ArgumentException.ThrowIfNullOrWhiteSpace(reflection);
        ArgumentNullException.ThrowIfNull(pathType);

        Book = book;
        Chapter = chapter;
        VerseRange = verseRange;
        Passage = passage;
        Context = context;
        Themes = themes;
        Reflection = reflection;
        PathType = pathType;
        CreatedAt = DateTime.UtcNow;
    }

    public static Lesson CreateChronological(LessonBuilder builder)
    {
        return new Lesson(
            book: builder.Book,
            chapter: builder.Chapter,
            verseRange: builder.VerseRange,
            passage: builder.Passage,
            context: builder.Context,
            themes: builder.Themes,
            reflection: builder.Reflection,
            pathType: LessonPathType.Chronological
        );
    }

    public string BookChapterVerseRange()
    {
        return $"{Book} {Chapter}:{VerseRange}";
    }
}

public class LessonBuilder
{
    public string Book { get; set; }
    public int Chapter { get; set; }
    public VerseRange  VerseRange { get; set; }
    public string Passage { get; set; }
    public string Context { get; set; }
    public string Themes { get; set; }
    public string Reflection { get; set; }

    public LessonBuilder(string book, int chapter, VerseRange verseRange, string passage, string context, string themes, string reflection)
    {
        Book = book;
        Chapter = chapter;
        VerseRange = verseRange;
        Passage = passage;
        Context = context;
        Themes = themes;
        Reflection = reflection;
    }
}
