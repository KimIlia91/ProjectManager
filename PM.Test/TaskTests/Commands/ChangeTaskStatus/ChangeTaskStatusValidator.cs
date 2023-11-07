using PM.Application.Common.Resources;
using PM.Application.Features.TaskContext.Commands.ChangeTaskStatus;
using PM.Domain.Common.Enums;
using PM.Test.Common.FakeRepositories;
using PM.Test.Common.FakeServices;

namespace PM.Test.TaskTests.Commands.ChangeTaskStatus;

public class ChangeTaskStatusValidator
{
    private readonly FakeTaskRepository _taskRepository;
    private readonly FakeCurrentUserService _currentUserService;

    public ChangeTaskStatusValidator()
    {
        _taskRepository = new FakeTaskRepository();
        _currentUserService = new FakeCurrentUserService();
    }

    [Fact]
    public async Task Validator_Should_ReturnNoErrors_WhenAllIsValid()
    {
        var command = new ChangeTaskStatusCommand()
        {
            TaskId = 1,
            Status = Status.InProgress
        };

        var validator = new ChangeTaskStatusCommandValidator(
            _taskRepository, _currentUserService);

        var result = await validator.ValidateAsync(command);

        Assert.True(result.IsValid);
        Assert.Empty(result.Errors);
    }

    [Fact]
    public async Task Validator_Should_ReturnRequiredError_WhenTaskIdIsEmpty()
    {
        var command = new ChangeTaskStatusCommand()
        {
            Status = Status.InProgress
        };

        var validator = new ChangeTaskStatusCommandValidator(
            _taskRepository, _currentUserService);

        var result = await validator.ValidateAsync(command);

        Assert.False(result.IsValid);
        Assert.Single(result.Errors);
        Assert.Equal(nameof(command.TaskId), result.Errors.First().PropertyName);
        Assert.Equal(ErrorsResource.Required, result.Errors.First().ErrorMessage);
    }

    [Fact]
    public async Task Validator_Should_ReturnNotFound_WhenTaskIsNotInDatabase()
    {
        var command = new ChangeTaskStatusCommand()
        {
            TaskId = 200,
            Status = Status.InProgress
        };

        var validator = new ChangeTaskStatusCommandValidator(
            _taskRepository, _currentUserService);

        var result = await validator.ValidateAsync(command);

        Assert.False(result.IsValid);
        Assert.Single(result.Errors);
        Assert.Equal(nameof(command.TaskId), result.Errors.First().PropertyName);
        Assert.Equal(ErrorsResource.NotFound, result.Errors.First().ErrorMessage);
    }

    [Fact]
    public async Task Validator_Should_ReturnNotFound_WhenTaskInDatabaseIsNotTaskOfCurrentUser()
    {
        var command = new ChangeTaskStatusCommand()
        {
            TaskId = 2,
            Status = Status.InProgress
        };

        var validator = new ChangeTaskStatusCommandValidator(
            _taskRepository, _currentUserService);

        var result = await validator.ValidateAsync(command);

        Assert.False(result.IsValid);
        Assert.Single(result.Errors);
        Assert.Equal(nameof(command.TaskId), result.Errors.First().PropertyName);
        Assert.Equal(ErrorsResource.NotFound, result.Errors.First().ErrorMessage);
    }

    [Fact]
    public async Task Validator_Should_ReturnRequired_WhenTaskStatusIsEmpty()
    {
        var command = new ChangeTaskStatusCommand()
        {
            TaskId = 1,
        };

        var validator = new ChangeTaskStatusCommandValidator(
            _taskRepository, _currentUserService);

        var result = await validator.ValidateAsync(command);

        Assert.False(result.IsValid);
        Assert.Single(result.Errors);
        Assert.Equal(nameof(command.Status), result.Errors.First().PropertyName);
        Assert.Equal(ErrorsResource.Required, result.Errors.First().ErrorMessage);
    }
}
