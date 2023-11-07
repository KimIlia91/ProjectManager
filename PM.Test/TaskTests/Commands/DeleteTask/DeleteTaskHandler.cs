using PM.Application.Features.TaskContext.Commands.DeleteTask;
using PM.Test.Common.FakeRepositories;

namespace PM.Test.TaskTests.Commands.DeleteTask;

public sealed class DeleteTaskHandler
{
    private readonly FakeTaskRepository _fakeTaskRepository;

    public DeleteTaskHandler()
    {
        _fakeTaskRepository = new FakeTaskRepository();
    }

    [Fact]
    public async Task Handler_Should_ReturnDeleteTashResult_WhenAllIsValid()
    {
        //Arrange
        var task = await _fakeTaskRepository
            .GetOrDeafaultAsync(t => t.Id == 1, CancellationToken.None);

        var command = new DeleteTaskCommand()
        {
            Task = task,
            TaskId = task!.Id
        };

        var handler = new DeleteTaskCommandHandler(_fakeTaskRepository);

        //Act
        var result = await handler.Handle(command, CancellationToken.None);

        //Assert
        Assert.False(result.IsError);
        Assert.Equal(command.TaskId, result.Value.TaskId);
    }
}
