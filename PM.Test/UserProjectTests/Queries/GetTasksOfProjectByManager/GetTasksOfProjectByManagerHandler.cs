using Moq;
using PM.Application.Common.Interfaces.ISercices;
using PM.Application.Features.TaskContext.Queries.GetTaskOfCurrentUser;
using PM.Application.Features.TaskContext.Queries.GetTasksOfProjectByManager;
using PM.Test.Common.Constants;
using PM.Test.Common.FakeRepositories;
using PM.Test.Common.FakeServices;

namespace PM.Test.UserProjectTests.Queries.GetTasksOfProjectByManager;

public sealed class GetTasksOfProjectByManagerHandler
{
    private readonly FakeTaskRepository _taskRepository;
    private readonly FakeCurrentUserService _currentUserService;

    public GetTasksOfProjectByManagerHandler()
    {
        _taskRepository = new FakeTaskRepository();
        _currentUserService = new FakeCurrentUserService();
    }

    [Fact]
    public async Task Handle_Should_ReturnTaskResult_WhenCurrentUserIsManagerOfProject()
    {
        // Arrange
        var query = new GetTasksOfProjectByManagerQuery() { ProjectId = 1 };
        var handler = new GetTasksOfProjectByManagerQueryHandler(
            _taskRepository, _currentUserService);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.False(result.IsError);
        Assert.NotNull(result.Value);
        Assert.Equal(TestDataConstants.TestTaskName1, result.Value.First().Name);
    }

    [Fact]
    public async Task Handle_Should_ReturnEmptyList_WhenCurrentUserIsNotManagerOfProject()
    {
        // Arrange
        var currentUserService = new Mock<ICurrentUserService>();
        currentUserService.Setup(x => x.UserId).Returns(2);

        var query = new GetTasksOfProjectByManagerQuery() { ProjectId = 1 };
        var handler = new GetTasksOfProjectByManagerQueryHandler(
            _taskRepository, currentUserService.Object);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.False(result.IsError);
        Assert.Empty(result.Value);
    }
}
