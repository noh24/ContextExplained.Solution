using ContextExplained.Core.Entities;
using ContextExplained.Core.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ContextExplained.Infrastructure.Configurations;

public class LessonConfiguration : IEntityTypeConfiguration<Lesson>
{
    public void Configure(EntityTypeBuilder<Lesson> builder)
    {
        builder.Property(l => l.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

        builder.Property(l => l.Book).IsRequired();
        builder.Property(l => l.Chapter).IsRequired();

        builder.OwnsOne(l => l.VerseRange, vr =>
        {
            vr.Property(v => v.Start).HasColumnName("VerseStart").IsRequired();
            vr.Property(v => v.End).HasColumnName("VerseEnd").IsRequired();
        });

        builder.Property(l => l.Passage).IsRequired();
        builder.Property(l => l.Context).IsRequired();
        builder.Property(l => l.Themes).IsRequired();
        builder.Property(l => l.Reflection).IsRequired();

        builder.OwnsOne(l => l.PathType, pt =>
        {
            pt.Property(p => p.Value).HasColumnName("PathType").IsRequired();
        });
    }
}
