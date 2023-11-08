using PM.Application.Features.TaskContext.Queries.GetTask;
using PM.Application.Features.TaskContext.Queries.GetTaskList;
using PM.Domain.Common.Enums;
using PM.Test.Common.Constants;
using PM.Test.Common.FakeRepositories;

namespace PM.Test.UserProjectTests.Queries.GetTaskList;

public sealed class GetTaskListHandler
{
    private readonly FakeTaskRepository _taskRepository;

    public GetTaskListHandler()
    {
        _taskRepository = new FakeTaskRepository();
    }

    [Fact]
    public async Task Handler_Should_ReturnTaskResult_WhenTaskIsInDatabase()
    {
        //Arrange
        var query = new GetTaskListQuery();
        var handler = new GetTaskListQueryHandler(_taskRepository);

        //Act
        var result = await handler.Handle(query, CancellationToken.None);

        //Assert
        Assert.False(result.IsError);
        Assert.NotEmpty(result.Value);
    }

    [Fact]
    public async Task Handler_Should_ReturnTwoTasks_WhenTaskFilterIsToDo()
    {
        // Arrange
        var query = new GetTaskListQuery();
        query.Filter.Status = Status.ToDo;
        var handler = new GetTaskListQueryHandler(_taskRepository);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.False(result.IsError);
        Assert.NotEmpty(result.Value);

        foreach (var task in result.Value)
        {
            Assert.Equal(Status.ToDo.ToString(), task.Status);
        }

        var expectedCount = 2;
        var actualCount = result.Value.Count;
        Assert.Equal(expectedCount, actualCount);
    }

    [Fact]
    public async Task Handler_Should_ReturnTasksOrderByDescending_WhenSortByName()
    {
        // Arrange
        var query = new GetTaskListQuery();
        query.SortBy = "Name.Desc";
        var handler = new GetTaskListQueryHandler(_taskRepository);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.False(result.IsError);
        Assert.NotEmpty(result.Value);

        var sortedTasks = result.Value.OrderByDescending(task => task.Name);
        Assert.True(result.Value.SequenceEqual(sortedTasks));
    }
}
