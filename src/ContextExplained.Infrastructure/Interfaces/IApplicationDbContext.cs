using ContextExplained.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace ContextExplained.Infrastructure.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Lesson> Lessons { get; }
    DbSet<LessonPath> LessonPaths { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
