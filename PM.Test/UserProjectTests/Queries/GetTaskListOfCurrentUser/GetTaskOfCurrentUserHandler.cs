using Moq;
using PM.Application.Common.Interfaces.ISercices;
using PM.Application.Features.TaskContext.Queries.GetTasksOfCurrentUser;
using PM.Infrastructure.Persistence.Repositories;
using PM.Test.Common.Constants;
using PM.Test.Common.FakeRepositories;
using PM.Test.Common.FakeServices;

namespace PM.Test.UserProjectTests.Queries.GetTaskListOfCurrentUser;

public sealed class GetTaskOfCurrentUserHandler
{
    private readonly FakeTaskRepository _taskRepository;
    private readonly FakeCurrentUserService _currentUserService;

    public GetTaskOfCurrentUserHandler()
    {
        _taskRepository = new FakeTaskRepository();
        _currentUserService = new FakeCurrentUserService();
    }

    [Fact]
    public async Task Handle_Should_ReturnTaskResults_WhenCurrentUserHasTasks()
    {
        // Arrange
        var handler = new GetTasksOfCurrentUserQueryHandler(_taskRepository, _currentUserService);
        var query = new GetTasksOfCurrentUserQuery();

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.False(result.IsError);
        Assert.NotNull(result.Value);
        Assert.Equal(TestDataConstants.TestTaskName1,
            result.Value.First(v => v.Name == TestDataConstants.TestTaskName1).Name);

        Assert.Null(result.Value.FirstOrDefault(v => 
            v.Name == TestDataConstants.TestTaskName3));
    }

    [Fact]
    public async Task Handle_Should_ReturnEmptyList_WhenCurrentUserHasNoTasks()
    {
        // Arrange
        var currentUserService = new Mock<ICurrentUserService>();
        currentUserService.Setup(x => x.UserId).Returns(123);

        var handler = new GetTasksOfCurrentUserQueryHandler(
            _taskRepository, currentUserService.Object);
        var query = new GetTasksOfCurrentUserQuery();

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.False(result.IsError);
        Assert.NotNull(result.Value);
        Assert.Empty(result.Value);
    }
}
