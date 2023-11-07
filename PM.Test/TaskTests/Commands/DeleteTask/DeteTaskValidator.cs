using PM.Application.Common.Resources;
using PM.Application.Features.TaskContext.Commands.DeleteTask;
using PM.Test.Common.FakeRepositories;
using PM.Test.Common.FakeServices;

namespace PM.Test.TaskTests.Commands.DeleteTask;

public sealed class DeteTaskValidator
{
    private readonly FakeTaskRepository _taskRepository;
    private readonly FakeCurrentUserService _currentUserService;

    public DeteTaskValidator()
    {
        _taskRepository = new FakeTaskRepository();
        _currentUserService = new FakeCurrentUserService();
    }

    [Fact]
    public async Task Validator_Should_ReturnNoErrors_WhenAllAreValid()
    {
        //Arrange
        var command = new DeleteTaskCommand()
        {
            TaskId = 1,
        };

        var validator = new DeleteTaskCommandValidator(
            _taskRepository, _currentUserService);

        //Act
        var result = await validator.ValidateAsync(command);

        //Assert
        Assert.True(result.IsValid);
        Assert.Empty(result.Errors);
    }

    [Fact]
    public async Task Validator_Should_ReturnNotFound_WhenTaskIsNotInDatabase()
    {
        //Arrange
        var command = new DeleteTaskCommand()
        {
            TaskId = 300,
        };

        var validator = new DeleteTaskCommandValidator(
            _taskRepository, _currentUserService);

        //Act
        var result = await validator.ValidateAsync(command);

        //Assert
        Assert.False(result.IsValid);
        Assert.Single(result.Errors);
        Assert.Equal(nameof(command.TaskId), result.Errors.First().PropertyName);
        Assert.Equal(ErrorsResource.NotFound, result.Errors.First().ErrorMessage);
    }

    [Fact]
    public async Task Validator_Should_ReturnNotFound_WhenCurrentUserIsNotManager()
    {
        //Arrange
        var command = new DeleteTaskCommand()
        {
            TaskId = 3,
        };

        var validator = new DeleteTaskCommandValidator(
            _taskRepository, _currentUserService);

        //Act
        var result = await validator.ValidateAsync(command);

        //Assert
        Assert.False(result.IsValid);
        Assert.Single(result.Errors);
        Assert.Equal(nameof(command.TaskId), result.Errors.First().PropertyName);
        Assert.Equal(ErrorsResource.NotFound, result.Errors.First().ErrorMessage);
    }

    [Fact]
    public async Task Validator_Should_ReturnNoErrors_WhenCurrentUserIsManager()
    {
        //Arrange
        var command = new DeleteTaskCommand()
        {
            TaskId = 2,
        };

        var validator = new DeleteTaskCommandValidator(
            _taskRepository, _currentUserService);

        //Act
        var result = await validator.ValidateAsync(command);

        //Assert
        Assert.True(result.IsValid);
        Assert.Empty(result.Errors);
    }
}
