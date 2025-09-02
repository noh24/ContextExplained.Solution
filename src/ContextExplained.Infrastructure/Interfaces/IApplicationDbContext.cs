using ContextExplained.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace ContextExplained.Infrastructure.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Lesson> Lessons { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
