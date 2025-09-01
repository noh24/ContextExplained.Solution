using ContextExplained.Core.Entities;
using ContextExplained.Core.DTOs;

namespace ContextExplained.Core.Interfaces;

public interface ILLMService
{
    Task<LessonDTO> GenerateNextLessonAsync(string previousLesson, string prompt);
}