using ContextExplained.Core.Entities;
using ContextExplained.Core.Interfaces;
using ContextExplained.Core.DTOs;
using Moq;
using ContextExplained.Core.ValueObjects;
using ContextExplained.Services;
using ContextExplained.Tests.Common;

namespace ContextExplained.Tests.Services;

public class LessonServiceTests
{
    private readonly Mock<ILessonRepository> _mockRepo = new();
    private readonly Mock<ILLMService> _mockLLM = new();
    private readonly Lesson _previousLesson = Lesson.Create(FakeLessonDTOGenerator.Create());

[Fact]
    public async Task GenerateAndSaveNextLessonAsync_ShouldCreateAndSaveLesson()
    {
        _mockRepo.Setup(r => r.GetPreviousLessonAsync())
            .ReturnsAsync(_previousLesson);

        var newLessonDto = FakeLessonDTOGenerator.Create();

        _mockLLM.Setup(s => s.GenerateNextLessonAsync(
                _previousLesson.BookChapterVerseRange(),
                It.IsAny<string>()))
            .ReturnsAsync(newLessonDto);

        var service = new LessonService(_mockRepo.Object, _mockLLM.Object);

        var result = await service.GenerateAndSaveNextLessonAsync("some prompt");

        Assert.Equal(newLessonDto.Book, result.Book);
        Assert.Equal(newLessonDto.Chapter, result.Chapter);
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
        var dto = FakeLessonDTOGenerator.Create();
        var lessons = new[]
        {
            Lesson.Create(dto)
        };

        _mockRepo.Setup(r => r.GetAllLessonsAsync())
            .ReturnsAsync(lessons);

        var service = new LessonService(_mockRepo.Object, _mockLLM.Object);

        var result = await service.GetAllLessonsAsync();

        Assert.Single(result);
        Assert.Equal(dto.Book, result.First().Book);
    }
}
