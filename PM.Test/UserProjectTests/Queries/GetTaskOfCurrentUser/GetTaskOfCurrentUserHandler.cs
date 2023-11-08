using PM.Application.Common.Resources;
using PM.Application.Features.TaskContext.Queries.GetTaskOfCurrentUser;
using PM.Test.Common.Constants;
using PM.Test.Common.FakeRepositories;
using PM.Test.Common.FakeServices;

namespace PM.Test.UserProjectTests.Queries.GetTaskOfCurrentUser;

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
    public async Task Handle_Should_ReturnTaskResult_WhenTaskExistsForCurrentUser()
    {
        // Arrange
        var query = new GetTaskOfCurrentUserQuery(1);
        var handler = new GetTaskOfUserCurrentQueryHandler(
            _taskRepository, _currentUserService);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.False(result.IsError);
        Assert.NotNull(result.Value);
        Assert.Equal(TestDataConstants.TestTaskName1, result.Value.Name);
    }

    [Fact]
    public async Task Handle_Should_ReturnTaskNotFound_WhenTaskInDatabaseNotExistsForCurrentUser()
    {
        // Arrange
        var query = new GetTaskOfCurrentUserQuery(3);
        var handler = new GetTaskOfUserCurrentQueryHandler(
            _taskRepository, _currentUserService);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.True(result.IsError);
        Assert.Equal(ErrorsResource.NotFound, result.Errors.First().Code);
        Assert.Equal(nameof(query.TaskId), result.Errors.First().Description);
    }

    [Fact]
    public async Task Handle_Should_ReturnTaskResult_WhenUserManagerOfProject()
    {
        // Arrange
        var query = new GetTaskOfCurrentUserQuery(2);
        var handler = new GetTaskOfUserCurrentQueryHandler(
            _taskRepository, _currentUserService);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.False(result.IsError);
        Assert.NotNull(result.Value);
        Assert.Equal(TestDataConstants.TestTaskName2, result.Value.Name);
    }
}
