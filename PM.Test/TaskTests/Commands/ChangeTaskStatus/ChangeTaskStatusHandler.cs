using PM.Application.Features.TaskContext.Commands.ChangeTaskStatus;
using PM.Domain.Common.Enums;
using PM.Test.Common.FakeRepositories;

namespace PM.Test.TaskTests.Commands.ChangeTaskStatus;

public sealed class ChangeTaskStatusHandler
{
    private readonly FakeTaskRepository _taskRepository;

    public ChangeTaskStatusHandler()
    {
        _taskRepository = new FakeTaskRepository();
    }

    [Fact]
    public async Task Handler_Should_ReturnChageStatusResult_WhenAllValid()
    {
        //Arrange
        var task = await _taskRepository
            .GetOrDeafaultAsync(t => t.Status == Status.ToDo, CancellationToken.None);

        var command = new ChangeTaskStatusCommand()
        {
            Task = task,
            TaskId = task.Id,
            Status = Status.InProgress
        };

        var handler = new ChangeTaskStatusCommandHandler(_taskRepository);

        //Act
        var result = await handler.Handle(command, CancellationToken.None);

        var expectedTask = await _taskRepository
            .GetOrDeafaultAsync(t => t.Id == command.TaskId &&
                t.Status == Status.InProgress, CancellationToken.None);

        //Assert
        Assert.False(result.IsError);
        Assert.Equal(command.TaskId, result.Value.TaskId);
        Assert.NotNull(expectedTask);
        Assert.Equal(command.Status, expectedTask.Status);
    }
}
