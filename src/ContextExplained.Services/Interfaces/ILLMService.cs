using ContextExplained.Services.DTOs;

namespace ContextExplained.Services.Interfaces;

public interface ILLMService
{
    Task<LessonDTO> GenerateNextLessonAsync(string previousLesson, string prompt);
}