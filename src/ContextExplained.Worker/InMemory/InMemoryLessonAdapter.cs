using ContextExplained.Core.Entities;
using ContextExplained.Core.Interfaces;

namespace ContextExplained.Worker.InMemory;

public class InMemoryLessonAdapter : ILessonRepository
{
    private readonly List<Lesson> _lessons = [];

    public Task AddLessonAsync(Lesson lesson)
    {
        _lessons.Add(lesson);
        return Task.CompletedTask;
    }

    public Task<IEnumerable<Lesson>> GetAllLessonsAsync()
    {
        return Task.FromResult<IEnumerable<Lesson>>(_lessons);
    }

    public Task<Lesson?> GetPreviousLessonAsync()
    {
        var latest = _lessons
            .OrderByDescending(l => l.Book)
            .ThenByDescending(l => l.Chapter)
            .ThenByDescending(l => l.VerseRange.End)
            .FirstOrDefault();
        return Task.FromResult(latest);
    }
}
