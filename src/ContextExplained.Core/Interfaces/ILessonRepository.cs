using ContextExplained.Core.Entities;

namespace ContextExplained.Core.Interfaces;

public interface ILessonRepository
{
    Task<Lesson?> GetPreviousLessonAsync();
    Task AddLessonAsync(Lesson lesson);
    Task<IEnumerable<Lesson>> GetAllLessonsAsync();
}

