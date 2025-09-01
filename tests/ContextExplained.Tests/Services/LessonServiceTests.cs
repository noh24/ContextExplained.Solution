using ContextExplained.Core.Entities;
using ContextExplained.Core.Interfaces;
using ContextExplained.Core.DTOs;
using Moq;
using ContextExplained.Core.ValueObjects;
using ContextExplained.Services;

namespace ContextExplained.Tests.Services;

public class LessonServiceTests
{
    private readonly Mock<ILessonRepository> _mockRepo = new();
    private readonly Mock<ILLMService> _mockLLM = new();
    private readonly Lesson _previousLesson = Lesson.Create(new LessonDTO
    {
        Book = "Genesis",
        Chapter = 1,
        VerseRange = new VerseRange(1, 10),
        Passage = "In the beginning...",
        Context = "Creation",
        Themes = "God's work",
        Reflection = "Reflect"
    });
    private readonly LessonDTO _newLessonDTO = new LessonDTO
    {
        Book = "Genesis",
        Chapter = 1,
        VerseRange = new VerseRange(11, 20),
        Passage = "Next passage",
        Context = "More creation",
        Themes = "God's work",
        Reflection = "Reflect more"
    };

[Fact]
    public async Task GenerateAndSaveNextLessonAsync_ShouldCreateAndSaveLesson()
    {
        _mockRepo.Setup(r => r.GetPreviousLessonAsync())
            .ReturnsAsync(_previousLesson);

        _mockLLM.Setup(s => s.GenerateNextLessonAsync(
                _previousLesson.BookChapterVerseRange(),
                It.IsAny<string>()))
            .ReturnsAsync(_newLessonDTO);

        var service = new LessonService(_mockRepo.Object, _mockLLM.Object);

        var result = await service.GenerateAndSaveNextLessonAsync("some prompt");

        Assert.Equal(_newLessonDTO.Book, result.Book);
        Assert.Equal(_newLessonDTO.Chapter, result.Chapter);
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
            Lesson.Create(_newLessonDTO)
        };

        _mockRepo.Setup(r => r.GetAllLessonsAsync())
            .ReturnsAsync(lessons);

        var service = new LessonService(_mockRepo.Object, _mockLLM.Object);

        var result = await service.GetAllLessonsAsync();

        Assert.Single(result);
        Assert.Equal("Genesis", result.First().Book);
    }
}
