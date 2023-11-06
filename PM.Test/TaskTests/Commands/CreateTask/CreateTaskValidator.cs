using PM.Application.Common.Resources;
using PM.Application.Features.TaskContext.Commands.CreateTask;
using PM.Domain.Common.Enums;
using PM.Test.Common.FakeRepositories;
using PM.Test.Common.FakeServices;
using Task = System.Threading.Tasks.Task;

namespace PM.Test.TaskTests.Commands.CreateTask;

public sealed class CreateTaskValidator
{
    private readonly FakeProjectRepository _projectRepository;
    private readonly FakeCurrentUserService _currentUserService;
    private readonly FakeUserRepository _userRepository;

    public CreateTaskValidator()
    {
        _currentUserService = new FakeCurrentUserService();
        _projectRepository = new FakeProjectRepository();
        _userRepository = new FakeUserRepository();
    }

    [Fact]
    public async Task Validator_Should_ReturnNotErrors_WhenAllAreValid()
    {
        //Arrange
        var command = new CreateTaskCommand()
        {
            Name = "Title",
            ExecutorId = 2,
            ProjectId = 1,
            Status = Status.ToDo,
            Priority = Priority.Medium
        };

        var validator = new CreateTaskCommandValidator(
            _userRepository, _projectRepository, _currentUserService);

        //Act
        var result = await validator.ValidateAsync(command);

        //Assert
        Assert.True(result.IsValid);
        Assert.Empty(result.Errors);
    }

    [Fact]
    public async Task Validator_Should_ReturnNotFoundError_WhenCurrentUserNotManagerOfProject()
    {
        //Arrange
        var command = new CreateTaskCommand()
        {
            Name = "Title",
            ExecutorId = 2,
            ProjectId = 2,
            Status = Status.ToDo,
            Priority = Priority.Medium
        };

        var validator = new CreateTaskCommandValidator(
            _userRepository, _projectRepository, _currentUserService);

        //Act
        var result = await validator.ValidateAsync(command);

        //Assert
        Assert.False(result.IsValid);
        Assert.Single(result.Errors);
        Assert.Equal(nameof(command.ProjectId), result.Errors.First().PropertyName);
        Assert.Equal(ErrorsResource.NotFound, result.Errors.First().ErrorMessage);
    }

    [Fact]
    public async Task Validator_Should_ReturnRequred_WhenProjectIdIsEmpty()
    {
        //Arrange
        var command = new CreateTaskCommand()
        {
            Name = "Title",
            ExecutorId = 2,
            Status = Status.ToDo,
            Priority = Priority.Medium
        };

        var validator = new CreateTaskCommandValidator(
            _userRepository, _projectRepository, _currentUserService);

        //Act
        var result = await validator.ValidateAsync(command);

        //Assert
        Assert.False(result.IsValid);
        Assert.Single(result.Errors);
        Assert.Equal(nameof(command.ProjectId), result.Errors.First().PropertyName);
        Assert.Equal(ErrorsResource.Required, result.Errors.First().ErrorMessage);
    }

    [Fact]
    public async Task Validator_Should_ReturnNotFound_WhenExecutorMustBeInProject()
    {
        //Arrange
        var command = new CreateTaskCommand()
        {
            Name = "Title",
            ExecutorId = 3,
            ProjectId = 1,
            Status = Status.ToDo,
            Priority = Priority.Medium
        };

        var validator = new CreateTaskCommandValidator(
            _userRepository, _projectRepository, _currentUserService);

        //Act
        var result = await validator.ValidateAsync(command);

        //Assert
        Assert.False(result.IsValid);
        Assert.Single(result.Errors);
        Assert.Equal(nameof(command.ExecutorId), result.Errors.First().PropertyName);
        Assert.Equal(ErrorsResource.NotFound, result.Errors.First().ErrorMessage);
    }

    [Fact]
    public async Task Validator_Should_ReturnNotFound_WhenExecutorIsNotInDatabase()
    {
        //Arrange
        var command = new CreateTaskCommand()
        {
            Name = "Title",
            ExecutorId = 100,
            ProjectId = 1,
            Status = Status.ToDo,
            Priority = Priority.Medium
        };

        var validator = new CreateTaskCommandValidator(
            _userRepository, _projectRepository, _currentUserService);

        //Act
        var result = await validator.ValidateAsync(command);

        //Assert
        Assert.False(result.IsValid);
        Assert.Single(result.Errors);
        Assert.Equal(nameof(command.ExecutorId), result.Errors.First().PropertyName);
        Assert.Equal(ErrorsResource.NotFound, result.Errors.First().ErrorMessage);
    }

    [Fact]
    public async Task Validator_Should_ReturnRequired_WhenNameIsEmpty()
    {
        //Arrange
        var command = new CreateTaskCommand()
        {
            ExecutorId = 2,
            ProjectId = 1,
            Status = Status.ToDo,
            Priority = Priority.Medium
        };

        var validator = new CreateTaskCommandValidator(
            _userRepository, _projectRepository, _currentUserService);

        //Act
        var result = await validator.ValidateAsync(command);

        //Assert
        Assert.False(result.IsValid);
        Assert.Single(result.Errors);
        Assert.Equal(nameof(command.Name), result.Errors.First().PropertyName);
        Assert.Equal(ErrorsResource.Required, result.Errors.First().ErrorMessage);
    }
}