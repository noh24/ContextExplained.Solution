using ContextExplained.Core.Entities;
using ContextExplained.Core.Interfaces;
using ContextExplained.Core.ValueObjects;
using ContextExplained.Infrastructure.Adapters;
using Microsoft.EntityFrameworkCore;

namespace ContextExplained.Infrastructure.Repositories;

public class LessonRepository : ILessonRepository
{
    private readonly ApplicationDbContext _dbContext;

    public LessonRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<Lesson?> GetPreviousLessonAsync()
    {
        if (!await _dbContext.LessonPaths.AnyAsync())
            throw new InvalidOperationException("No LessonPaths exist in the system.");

        return await _dbContext.Lessons
            .Join(
                _dbContext.LessonPaths,
                lesson => new { lesson.PathType, lesson.Book },
                path => new { path.PathType, path.Book },
                (lesson, path) => new { Lesson = lesson, path.Sequence}
            )
            .Where(x => x.Lesson.PathType == LessonPathType.Chronological)
            .OrderByDescending(x => x.Sequence)
            .ThenByDescending(x => x.Lesson.Chapter)
            .ThenByDescending(x => x.Lesson.VerseRange.End)
            .Select(x => x.Lesson)
            .FirstOrDefaultAsync();
    }

    async Task ILessonRepository.AddLessonAsync(Lesson lesson)
    {
        await _dbContext.Lessons.AddAsync(lesson);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<Lesson>> GetAllLessonsAsync()
    {
        if (!await _dbContext.LessonPaths.AnyAsync())
            throw new InvalidOperationException("No LessonPaths exist in the system.");

        return await _dbContext.Lessons
            .Join(_dbContext.LessonPaths,
                lesson => new { lesson.PathType, lesson.Book },
                path => new { path.PathType, path.Book },
                (lesson, path) => new { Lesson = lesson, path.Sequence, VerseEnd = lesson.VerseRange.End }
            )
            .AsNoTracking()
            .Where(x => x.Lesson.PathType == LessonPathType.Chronological)
            .OrderByDescending(x => x.Sequence)
            .ThenByDescending(x => x.Lesson.Chapter)
            .ThenByDescending(x => x.VerseEnd)
            .Select(x => x.Lesson)
            .ToListAsync();
    }
}
