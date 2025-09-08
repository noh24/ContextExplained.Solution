using ContextExplained.Core.Entities;
using ContextExplained.Core.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;

namespace ContextExplained.Infrastructure.Configurations;

public class LessonPathConfiguration : IEntityTypeConfiguration<LessonPath>
{
    public void Configure(EntityTypeBuilder<LessonPath> builder)
    {
        builder.HasKey(lp => lp.Id);
        builder.Property(lp => lp.Id).ValueGeneratedOnAdd();

        builder.Property(lp => lp.PathType)
            .HasConversion(
                v => v.Value, // to DB (string)
                v => new LessonPathType(v) // from DB (value object)
            ).HasColumnName("PathType")
            .IsRequired();


        builder.Property(lp => lp.Book).IsRequired();
        builder.Property(lp => lp.Sequence).IsRequired();
    }
}
