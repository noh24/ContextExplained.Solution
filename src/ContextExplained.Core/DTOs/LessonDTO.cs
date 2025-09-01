using ContextExplained.Core.ValueObjects;

namespace ContextExplained.Core.DTOs;

public record LessonDTO
{
    public string Book { get; set; } = string.Empty;
    public int Chapter { get; set; }
    public required VerseRange VerseRange { get; set; }
    public string Passage { get; set; } = string.Empty;
    public string Context { get; set; } = string.Empty;
    public string Themes { get; set; } = string.Empty;
    public string Reflection { get; set; } = string.Empty;
}
