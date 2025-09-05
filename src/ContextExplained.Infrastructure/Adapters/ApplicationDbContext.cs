using ContextExplained.Core.Entities;
using ContextExplained.Core.ValueObjects;
using ContextExplained.Infrastructure.Configurations;
using ContextExplained.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ContextExplained.Infrastructure.Adapters;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Lesson> Lessons => Set<Lesson>();
    public DbSet<LessonPath> LessonPaths => Set<LessonPath>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new LessonConfiguration());
        modelBuilder.ApplyConfiguration(new LessonPathConfiguration());
    }
}

