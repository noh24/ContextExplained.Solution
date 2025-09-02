using ContextExplained.Core.Entities;
using ContextExplained.Infrastructure.Configurations;
using ContextExplained.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ContextExplained.Infrastructure.Adapters;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Lesson> Lessons => Set<Lesson>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new LessonConfiguration());
    }
}

