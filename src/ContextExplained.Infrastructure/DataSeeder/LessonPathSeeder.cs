using ContextExplained.Core.Entities;
using ContextExplained.Core.ValueObjects;
using ContextExplained.Infrastructure.Adapters;

namespace ContextExplained.Infrastructure.DataSeeder;

public static class LessonPathSeeder
{
    public static async Task SeedAsync(ApplicationDbContext db)
    {
        if (!db.Set<LessonPath>().Any())
        {
            var chronologicalPath = new List<LessonPath>();
            foreach (var (book, sequence) in LessonPathsArchive.Chronological)
            {
                chronologicalPath.Add(new LessonPath(LessonPathType.Chronological, book, sequence));
            }

            db.AddRange(chronologicalPath);
            await db.SaveChangesAsync();
        }
    }

}
