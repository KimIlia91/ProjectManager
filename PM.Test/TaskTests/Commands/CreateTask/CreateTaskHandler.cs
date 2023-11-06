using Microsoft.EntityFrameworkCore;
using PM.Application.Features.TaskContext.Commands.CreateTask;
using PM.Test.Common.FakeRepositories;
using PM.Domain.Common.Enums;
using PM.Test.Common.FakeServices;
using Xunit.Abstractions;

namespace PM.Test.TaskTests.Commands.CreateTask;

public sealed class CreateTaskHandler
{
    private readonly FakeTaskRepositpry _taskRepositpry;
    private readonly FakeUserRepository _userRepository;
    private readonly FakeProjectRepository _projectRepository;
    private readonly FakeCurrentUserService _currentUserService;
    private readonly FakeCurrentUserIsSupervisorService _userIsSupervisorService;

    public CreateTaskHandler()
    {
        _taskRepositpry = new FakeTaskRepositpry();
        _userRepository = new FakeUserRepository();
        _projectRepository = new FakeProjectRepository();
        _currentUserService = new FakeCurrentUserService();
        _userIsSupervisorService = new FakeCurrentUserIsSupervisorService();
    }

    [Fact]
    public async Task Handler_Should_ReturnCreateTaskResult_WhenTaskCreateSuccess()
    {
        //Arrange
        var executor = await _userRepository
            .GetOrDeafaultAsync(t => t.Id == 2, CancellationToken.None);

        var project = await _projectRepository
            .GetOrDeafaultAsync(t => t.Id == 1, CancellationToken.None);

        var command = new CreateTaskCommand()
        {
            Name = "Title",
            Executor = executor,
            ExecutorId = 1,
            Project = project,
            ProjectId = 1,
            Status = Status.ToDo,
            Priority = Priority.Medium
        };

        var handler = new CreateTaskCommandHandler(
            _taskRepositpry, _userRepository, _currentUserService);
            
        //Act
        var result = await handler.Handle(command, CancellationToken.None);

        var expectedTask = await _taskRepositpry
           .GetOrDeafaultAsync(t => t.Name == command.Name &&
                t.Executor.Id == command.ExecutorId &&
                t.Author.Id == _currentUserService.UserId &&
                t.Status == command.Status &&
                t.Priority == command.Priority, CancellationToken.None);

        //Assert
        //Assert.False(result.IsError);
        Assert.NotNull(expectedTask);
    }
}
