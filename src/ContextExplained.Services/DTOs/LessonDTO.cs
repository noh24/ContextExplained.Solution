using ContextExplained.Core.ValueObjects;

namespace ContextExplained.Services.DTOs;

public record LessonDTO
{
    public required string Book { get; set; } = string.Empty;
    public required int Chapter { get; set; }
    public required VerseRange VerseRange { get; set; }
    public required string Passage { get; set; } = string.Empty;
    public required string Context { get; set; } = string.Empty;
    public required string Themes { get; set; } = string.Empty;
    public required string Reflection { get; set; } = string.Empty;
}
