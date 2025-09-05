using ContextExplained.Core.ValueObjects;

namespace ContextExplained.Core.Entities;

public class LessonPath
{
    public Guid Id { get; private set; }
    public LessonPathType PathType { get; private set; }
    public string Book { get; private set; }
    public int Sequence { get; private set; }

    private LessonPath() { }

    public LessonPath(LessonPathType pathType, string book, int sequence)
    {
        ArgumentNullException.ThrowIfNull(pathType);
        ArgumentException.ThrowIfNullOrWhiteSpace(book);

        if (sequence < 0) throw new ArgumentException("Sequence must not be less than 0.");

        PathType = pathType;
        Book = book;
        Sequence = sequence;
    }
}
