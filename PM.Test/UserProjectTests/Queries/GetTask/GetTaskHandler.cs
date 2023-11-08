using PM.Application.Common.Resources;
using PM.Application.Features.TaskContext.Queries.GetTask;
using PM.Test.Common.Constants;
using PM.Test.Common.FakeRepositories;

namespace PM.Test.UserProjectTests.Queries.GetTask;

public class GetTaskHandler
{
    private readonly FakeTaskRepository _taskRepository;

    public GetTaskHandler()
    {
        _taskRepository = new FakeTaskRepository();
    }

    [Fact]
    public async Task Handler_Should_ReturnTaskResult_WhenTaskIsInDatabase()
    {
        //Arrange
        var query = new GetTaskQuery(1);
        var handler = new GetTaskQueryHandler(_taskRepository);

        //Act
        var result = await handler.Handle(query, CancellationToken.None);

        //Assert
        Assert.False(result.IsError);
        Assert.Equal(TestDataConstants.TestTaskName, result.Value.Name);
        Assert.Equal(TestDataConstants.TestTaskStatus.ToString(), result.Value.Status);
        Assert.Equal(TestDataConstants.TestTaskPriority, result.Value.Priority);
    }

    [Fact]
    public async Task Handler_Should_ReturnNotFound_WhenTaskIsNotInDatabase()
    {
        //Arrange
        var query = new GetTaskQuery(100);
        var handler = new GetTaskQueryHandler(_taskRepository);

        //Act
        var result = await handler.Handle(query, CancellationToken.None);

        //Assert
        Assert.True(result.IsError);
        Assert.Equal(ErrorsResource.NotFound, result.Errors.First().Code);
        Assert.Equal(nameof(query.Id), result.Errors.First().Description);
    }
}
