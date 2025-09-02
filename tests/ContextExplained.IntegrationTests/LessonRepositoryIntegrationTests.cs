using ContextExplained.Core.DTOs;
using ContextExplained.Core.Entities;
using ContextExplained.Core.ValueObjects;
using ContextExplained.Infrastructure.Adapters;
using ContextExplained.Infrastructure.Repositories;
using ContextExplained.Tests.Common;
using Microsoft.EntityFrameworkCore;

namespace ContextExplained.IntegrationTests;

public class LessonRepositoryIntegrationTests
{
    private ApplicationDbContext GetInMemoryDbContext()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        return new ApplicationDbContext(options);
    }

    [Fact]
    public async Task GetAllLessonsAsync_Returns_AllLessons()
    {
        using var dbContext = GetInMemoryDbContext();
        var repository = new LessonRepository(dbContext);

        var lessonDtos = FakeLessonDTOGenerator.CreateMany(2).ToList();
        var lesson1 = Lesson.Create(lessonDtos[0]);
        var lesson2 = Lesson.Create(lessonDtos[1]);

        await dbContext.Lessons.AddRangeAsync(lesson1, lesson2);
        await dbContext.SaveChangesAsync();

        var lessons = await repository.GetAllLessonsAsync();

        Assert.Equal(2, lessons.Count());
        Assert.Contains(lessons, l => l.Passage == lessonDtos[0].Passage);
        Assert.Contains(lessons, l => l.Passage == lessonDtos[1].Passage);
    }
}
