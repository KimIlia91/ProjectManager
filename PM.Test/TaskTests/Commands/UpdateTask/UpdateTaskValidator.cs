using Moq;
using PM.Application.Common.Interfaces.ISercices;
using PM.Application.Common.Resources;
using PM.Application.Features.TaskContext.Commands.UpdateTask;
using PM.Test.Common.FakeRepositories;
using PM.Test.Common.FakeServices;

namespace PM.Test.TaskTests.Commands.UpdateTask;

public sealed class UpdateTaskValidator
{
    private readonly FakeTaskRepository _taskRepository;
    private readonly FakeUserRepository _userRepository;
    private readonly FakeCurrentUserService _currentUser;

    public UpdateTaskValidator()
    {
        _taskRepository = new FakeTaskRepository();
        _userRepository = new FakeUserRepository();
        _currentUser = new FakeCurrentUserService();
    }

    [Fact]
    public async Task Validator_Should_ReturnNoErros_WhenAllAreValid()
    {
        //Arrange
        var command = new UpdateTaskCommand()
        {
            Id = 1,
            Name = "Task update",
            Comment = "Comment update",
            ExecutorId = 2,
            AuthorId = 1,
            Status = Domain.Common.Enums.Status.Done,
            Priority = Domain.Common.Enums.Priority.High
        };

        var validator = new UpdateTaskCommandValidator(
            _taskRepository, _userRepository, _currentUser);

        //Act
        var result = await validator.ValidateAsync(command);

        //Assert
        Assert.True(result.IsValid);
    }


    [Fact]
    public async Task Validator_Should_ReturnExecutorIdNotFound_WhenExecutorNotInProject()
    {
        //Arrange
        var command = new UpdateTaskCommand()
        {
            Id = 1,
            Name = "Task update",
            Comment = "Comment update",
            ExecutorId = 3,
            AuthorId = 1,
            Status = Domain.Common.Enums.Status.Done,
            Priority = Domain.Common.Enums.Priority.High
        };

        var validator = new UpdateTaskCommandValidator(
            _taskRepository, _userRepository, _currentUser);

        //Act
        var result = await validator.ValidateAsync(command);

        //Assert
        Assert.False(result.IsValid);
        Assert.Single(result.Errors);
        Assert.Equal(nameof(command.ExecutorId), result.Errors.First().PropertyName);
        Assert.Equal(ErrorsResource.NotFound, result.Errors.First().ErrorMessage);
    }

    [Fact]
    public async Task Validator_Should_ReturnExecutorIdNotFound_WhenExecutorNotInDatabase()
    {
        //Arrange
        var command = new UpdateTaskCommand()
        {
            Id = 1,
            Name = "Task update",
            Comment = "Comment update",
            ExecutorId = 3500,
            AuthorId = 1,
            Status = Domain.Common.Enums.Status.Done,
            Priority = Domain.Common.Enums.Priority.High
        };

        var validator = new UpdateTaskCommandValidator(
            _taskRepository, _userRepository, _currentUser);

        //Act
        var result = await validator.ValidateAsync(command);

        //Assert
        Assert.False(result.IsValid);
        Assert.Single(result.Errors);
        Assert.Equal(nameof(command.ExecutorId), result.Errors.First().PropertyName);
        Assert.Equal(ErrorsResource.NotFound, result.Errors.First().ErrorMessage);
    }

    [Fact]
    public async Task Validator_Should_ReturnIdNotFound_WhenCurrentUserIsNotManagerOfProject()
    {
        //Arrange
        var command = new UpdateTaskCommand()
        {
            Id = 1,
            Name = "Task update",
            Comment = "Comment update",
            ExecutorId = 3500,
            AuthorId = 1,
            Status = Domain.Common.Enums.Status.Done,
            Priority = Domain.Common.Enums.Priority.High
        };

        var currentUserService = new Mock<ICurrentUserService>();
        currentUserService.Setup(service => service.UserId).Returns(2);

        var validator = new UpdateTaskCommandValidator(
            _taskRepository, _userRepository, currentUserService.Object);

        //Act
        var result = await validator.ValidateAsync(command);

        //Assert
        Assert.False(result.IsValid);
        Assert.Single(result.Errors);
        Assert.Equal(nameof(command.Id), result.Errors.First().PropertyName);
        Assert.Equal(ErrorsResource.NotFound, result.Errors.First().ErrorMessage);
    }

    [Fact]
    public async Task Validator_Should_ReturnSuccessResult_WhenCurrentUserIsSupervisor()
    {
        //Arrange
        var command = new UpdateTaskCommand()
        {
            Id = 1,
            Name = "Task update",
            Comment = "Comment update",
            ExecutorId = 2,
            AuthorId = 1,
            Status = Domain.Common.Enums.Status.Done,
            Priority = Domain.Common.Enums.Priority.High
        };

        var currentUserService = new Mock<ICurrentUserService>();
        currentUserService.Setup(service => service.IsSupervisor).Returns(true);

        var validator = new UpdateTaskCommandValidator(
            _taskRepository, _userRepository, currentUserService.Object);

        //Act
        var result = await validator.ValidateAsync(command);

        //Assert
        Assert.True(result.IsValid);
    }

    [Fact]
    public async Task Validator_Should_ReturnIdNotFound_When()
    {
        //Arrange
        var command = new UpdateTaskCommand()
        {
            Id = 1,
            Name = "Task update",
            Comment = "Comment update",
            ExecutorId = 3500,
            AuthorId = 1,
            Status = Domain.Common.Enums.Status.Done,
            Priority = Domain.Common.Enums.Priority.High
        };

        var currentUserService = new Mock<ICurrentUserService>();
        currentUserService.Setup(service => service.UserId).Returns(2);

        var validator = new UpdateTaskCommandValidator(
            _taskRepository, _userRepository, currentUserService.Object);

        //Act
        var result = await validator.ValidateAsync(command);

        //Assert
        Assert.False(result.IsValid);
        Assert.Single(result.Errors);
        Assert.Equal(nameof(command.Id), result.Errors.First().PropertyName);
        Assert.Equal(ErrorsResource.NotFound, result.Errors.First().ErrorMessage);
    }
}
