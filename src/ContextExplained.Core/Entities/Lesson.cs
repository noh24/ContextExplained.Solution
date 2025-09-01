using ContextExplained.Core.DTOs;
using ContextExplained.Core.ValueObjects;

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
    public DateTime CreatedAt { get; private set; }

    private Lesson() { } // EF Core
    private Lesson(string book, int chapter, VerseRange verseRange, string passage, string context, string themes, string reflection)
    {
        ArgumentNullException.ThrowIfNullOrWhiteSpace(book);
        if (chapter <= 0) throw new ArgumentException(null, nameof(chapter));
        ArgumentNullException.ThrowIfNull(verseRange);
        ArgumentNullException.ThrowIfNullOrWhiteSpace(passage);
        ArgumentNullException.ThrowIfNullOrWhiteSpace(context);
        ArgumentNullException.ThrowIfNullOrWhiteSpace(themes);
        ArgumentNullException.ThrowIfNullOrWhiteSpace(reflection);

        Id = Guid.NewGuid();
        Book = book;
        Chapter = chapter;
        VerseRange = verseRange;
        Passage = passage;
        Context = context;
        Themes = themes;
        Reflection = reflection;
        CreatedAt = DateTime.UtcNow;
    }

    public string BookChapterVerseRange()
    {
        return $"{Book} {Chapter}:{VerseRange}";
    }

    public static Lesson Create(LessonDTO dto)
    {
        ArgumentNullException.ThrowIfNull(dto);

        return new Lesson(
            book: dto.Book,
            chapter: dto.Chapter,
            verseRange: dto.VerseRange,
            passage: dto.Passage,
            context: dto.Context,
            themes: dto.Themes,
            reflection: dto.Reflection
        );
    }
}
