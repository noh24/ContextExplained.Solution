namespace ContextExplained.Core.ValueObjects;

public sealed class LessonPathType
{
    public string Value { get; }

    public LessonPathType(string value) => Value = value;

    public static readonly LessonPathType Chronological = new("Chronological");

    public override string ToString() => Value;
}
