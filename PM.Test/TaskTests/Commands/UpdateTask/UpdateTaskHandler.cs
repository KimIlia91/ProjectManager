using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using PM.Application.Common.Interfaces.IRepositories;
using PM.Application.Features.TaskContext.Commands.UpdateTask;
using PM.Test.Common.Constants;
using PM.Test.Common.FakeRepositories;

namespace PM.Test.TaskTests.Commands.UpdateTask;

public sealed class UpdateTaskHandler
{
    private readonly FakeTaskRepository _taskRepository;
    private readonly IMapper _mapper;

    public UpdateTaskHandler()
    {
        _taskRepository = new FakeTaskRepository();
        _mapper = new Mapper();
    }

    [Fact]
    public async Task Handler_Should_ReturnUpdateTaskResultSuccess_WhenAllAreValid()
    {
        //Arrange
        var task = await _taskRepository.Context.Tasks.FirstAsync();
        var executor = await _taskRepository.Context.Users.FirstAsync();
        var author = await _taskRepository.Context.Users.LastAsync();
        var command = new UpdateTaskCommand()
        {
            Executor = executor,
            Task = task,
            Author = author,
            Id = task.Id,
            Name = "Task update",
            Comment = "Comment update",
            ExecutorId = executor.Id,
            AuthorId = author.Id,
            Status = Domain.Common.Enums.Status.Done,
            Priority = Domain.Common.Enums.Priority.High
        };

        var handler = new UpdateTaskCommandHandler(_taskRepository, _mapper);

        //Act
        var result = await handler.Handle(command, CancellationToken.None);
        var expectedTask = await _taskRepository
            .GetOrDeafaultAsync(t => t.Name == command.Name, CancellationToken.None);

        //Assert
        Assert.False(result.IsError);
        Assert.NotNull(expectedTask);
        Assert.Equal(command.Name, expectedTask.Name);
        Assert.Equal(command.Comment, expectedTask.Comment);
        Assert.Equal(command.Status, expectedTask.Status);
        Assert.Equal(command.Priority, expectedTask.Priority);
    }
}
