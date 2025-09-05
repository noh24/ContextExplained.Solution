using ContextExplained.Core.Entities;
using ContextExplained.Core.Interfaces;
using ContextExplained.Services.DTOs;
using ContextExplained.Services.Interfaces;
using Moq;
using ContextExplained.Core.ValueObjects;
using ContextExplained.Services;
using ContextExplained.Tests.Common;

namespace ContextExplained.Tests.Services;

public class LessonServiceTests
{
    private readonly Mock<ILessonRepository> _mockRepo = new();
    private readonly Mock<ILLMService> _mockLLM = new();
    private readonly Lesson _previousLesson = FakeLessonGenerator.CreateChronological();

[Fact]
    public async Task GenerateAndSaveNextLessonAsync_ShouldCreateAndSaveLesson()
    {
        _mockRepo.Setup(r => r.GetPreviousLessonAsync())
            .ReturnsAsync(_previousLesson);

        var newLesson = FakeLessonGenerator.CreateChronological();

        _mockLLM.Setup(s => s.GenerateNextLessonAsync(
                _previousLesson.BookChapterVerseRange(),
                It.IsAny<string>()))
            .ReturnsAsync(new LessonDTO { 
                Book = newLesson.Book,
                Chapter = newLesson.Chapter,
                VerseRange = newLesson.VerseRange,
                Passage = newLesson.Passage,
                Context = newLesson.Context,
                Themes = newLesson.Themes,
                Reflection = newLesson.Reflection
            });

        var service = new LessonService(_mockRepo.Object, _mockLLM.Object);

        var result = await service.GenerateAndSaveNextLessonAsync("some prompt");

        Assert.Equal(newLesson.Book, result.Book);
        Assert.Equal(newLesson.Chapter, result.Chapter);
        Assert.Equal(newLesson.PathType, result.PathType);
        _mockRepo.Verify(r => r.AddLessonAsync(It.IsAny<Lesson>()), Times.Once);
    }

    [Fact]
    public async Task GenerateAndSaveNextLessonAsync_ShouldThrow_WhenNoPreviousLesson()
    {
        _mockRepo.Setup(r => r.GetPreviousLessonAsync())
            .ReturnsAsync((Lesson?)null);

        var service = new LessonService(_mockRepo.Object, _mockLLM.Object);

        await Assert.ThrowsAsync<InvalidOperationException>(
            () => service.GenerateAndSaveNextLessonAsync("prompt")
        );
    }

    [Fact]
    public async Task GetAllLessonsAsync_ShouldReturnAllLessons()
    {
        var lessons = new[]
        {
            FakeLessonGenerator.CreateChronological()
        };

        _mockRepo.Setup(r => r.GetAllLessonsAsync())
            .ReturnsAsync(lessons);

        var service = new LessonService(_mockRepo.Object, _mockLLM.Object);

        var result = await service.GetAllLessonsAsync();

        Assert.Single(result);
        Assert.Equal(lessons[0].Book, result.First().Book);
    }
}
