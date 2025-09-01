using ContextExplained.Core.DTOs;
using ContextExplained.Core.Interfaces;
using ContextExplained.Core.ValueObjects;
namespace ContextExplained.Tests.Mocks;

internal class MockLLMService : ILLMService
{
    public Task<LessonDTO> GenerateNextLessonAsync(string previousLesson, string prompt)
    {
        LessonDTO lessonDTO = new()
        {
            Book = "Genesis",
            Chapter = 1,
            VerseRange = new VerseRange(1, 1),
            Passage = "In the beginning...",
            Context = "This is the context...",
            Themes = "This is the theme...",
            Reflection = "Reflect on this..."

        };
        return Task.FromResult(lessonDTO);
    }
}
