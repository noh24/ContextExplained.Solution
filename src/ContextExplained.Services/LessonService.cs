using ContextExplained.Core.Entities;
using ContextExplained.Core.Interfaces;

namespace ContextExplained.Services;

public class LessonService
{
    private readonly ILessonRepository _lessonRepo;
    private readonly ILLMService _llmService;

    public LessonService(ILessonRepository lessonRepo, ILLMService llmService)
    {
        _lessonRepo = lessonRepo;
        _llmService = llmService;
    }
    
    public async Task<Lesson> GenerateAndSaveNextLessonAsync(string prompt)
    {
        var previousLesson = await _lessonRepo.GetPreviousLessonAsync() ??
            throw new InvalidOperationException("No previous lesson found.");

        var result = await _llmService.GenerateNextLessonAsync(previousLesson.BookChapterVerseRange(), prompt);

        var newLesson = Lesson.Create(result);

        await _lessonRepo.AddLessonAsync(newLesson);

        return newLesson;
    }

    public async Task<IEnumerable<Lesson>> GetAllLessonsAsync()
    {
        return await _lessonRepo.GetAllLessonsAsync();
    }
}
