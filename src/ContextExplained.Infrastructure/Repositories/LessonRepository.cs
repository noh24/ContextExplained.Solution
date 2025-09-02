using ContextExplained.Core.Entities;
using ContextExplained.Core.Interfaces;
using ContextExplained.Core.ValueObjects;
using ContextExplained.Infrastructure.Adapters;
using Microsoft.EntityFrameworkCore;

namespace ContextExplained.Infrastructure.Repositories;

public class LessonRepository : ILessonRepository
{
    private readonly ApplicationDbContext _db;

    public LessonRepository(ApplicationDbContext db)
    {
        _db = db;
    }
    public async Task<Lesson?> GetPreviousLessonAsync()
    {
        return await _db.Lessons
            .OrderByDescending(l => LessonPaths.CanonicalOrder[l.Book])
            .ThenBy(l => l.Chapter)
            .ThenBy(l => l.VerseRange.Start)
            .FirstOrDefaultAsync();
    }

    async Task ILessonRepository.AddLessonAsync(Lesson lesson)
    {
        await _db.Lessons.AddAsync(lesson);
        await _db.SaveChangesAsync();
    }

    public async Task<IEnumerable<Lesson>> GetAllLessonsAsync()
    {
        return await _db.Lessons
            .AsNoTracking()
            .OrderBy(l => LessonPaths.CanonicalOrder[l.Book])
            .ThenBy(l => l.Chapter)
            .ThenBy(l => l.VerseRange.Start)
            .ToListAsync();
    }
}
