using PM.Application.Common.Resources;
using PM.Application.Features.EmployeeProjectsContext.Commands.AddEmployeeToProject;
using PM.Test.Common.FakeRepositories;
using PM.Test.Common.FakeServices;

namespace PM.Test.UserProjectTests.Commands.AddUserToProject;

public sealed class AddUserToProjectValidator
{
    private readonly FakeProjectRepository _projectRepository;
    private readonly FakeCurrentUserService _currentUser;
    private readonly FakeUserRepository _userRepository;
    private readonly FakeCurrentUserIsSupervisorService _userIsSupervisorService;

    public AddUserToProjectValidator()
    {
        _projectRepository = new FakeProjectRepository();
        _currentUser = new FakeCurrentUserService();
        _userRepository = new FakeUserRepository();
        _userIsSupervisorService = new FakeCurrentUserIsSupervisorService();
    }

    [Fact]
    public async Task Validator_Should_ReturnNoErrors_WhenUserIsNotInProjectYet()
    {
        //Arrange
        var command = new AddUserToProjectCommand()
        {
            UserId = 3,
            ProjectId = 1,
        };

        var validator = new AddUserToProjectCommandValidator(
            _projectRepository, _userRepository, _currentUser);

        //Act
        var result = await validator.ValidateAsync(command, CancellationToken.None);

        //Assert
        Assert.True(result.IsValid);
    }

    [Fact]
    public async Task Validator_Should_ReturnNoErrors_WhenCurrentUserIsSupervisor()
    {
        //Arrange
        var command = new AddUserToProjectCommand()
        {
            UserId = 3,
            ProjectId = 2,
        };

        var validator = new AddUserToProjectCommandValidator(
            _projectRepository, _userRepository, _userIsSupervisorService);

        //Act
        var result = await validator.ValidateAsync(command, CancellationToken.None);

        //Assert
        Assert.True(result.IsValid);
        Assert.Empty(result.Errors);
    }

    [Fact]
    public async Task Validator_Should_ReturnUserIsAlreadyInProjectError_WhenUserIsAlreadyInProject()
    {
        //Arrange
        var command = new AddUserToProjectCommand()
        {
            UserId = 2,
            ProjectId = 1,
        };

        var validator = new AddUserToProjectCommandValidator(
            _projectRepository, _userRepository, _currentUser);

        //Act
        var result = await validator.ValidateAsync(command, CancellationToken.None);

        //Assert
        Assert.False(result.IsValid);
        Assert.Single(result.Errors);
        Assert.Equal(ErrorsResource.UserInProject, result.Errors.First().ErrorMessage);
    }

    [Fact]
    public async Task Validator_Should_ReturnNotFoundError_WhenCurrentUserIsNotManagerOfProject()
    {
        //Arrange
        var command = new AddUserToProjectCommand()
        {
            UserId = 3,
            ProjectId = 2,
        };

        var validator = new AddUserToProjectCommandValidator(
            _projectRepository, _userRepository, _currentUser);

        //Act
        var result = await validator.ValidateAsync(command, CancellationToken.None);

        //Assert
        Assert.False(result.IsValid);
        Assert.Single(result.Errors);
        Assert.Equal(nameof(command.ProjectId), result.Errors.First().PropertyName);
        Assert.Equal(ErrorsResource.NotFound, result.Errors.First().ErrorMessage);
    }

    [Fact]
    public async Task Validator_Should_ReturnNotFoundError_WhenUserNotFoundInDatabase()
    {
        //Arrange
        var command = new AddUserToProjectCommand()
        {
            UserId = 400,
            ProjectId = 1,
        };

        var validator = new AddUserToProjectCommandValidator(
            _projectRepository, _userRepository, _currentUser);

        //Act
        var result = await validator.ValidateAsync(command, CancellationToken.None);

        //Assert
        Assert.False(result.IsValid);
        Assert.Single(result.Errors);
        Assert.Equal(nameof(command.UserId), result.Errors.First().PropertyName);
        Assert.Equal(ErrorsResource.NotFound, result.Errors.First().ErrorMessage);
    }

    [Fact]
    public async Task Validator_Should_ReturnRequiredUserId_WhenUserIdIsEmpty()
    {
        //Arrange
        var command = new AddUserToProjectCommand()
        {
            ProjectId = 1,
        };

        var validator = new AddUserToProjectCommandValidator(
            _projectRepository, _userRepository, _currentUser);

        //Act
        var result = await validator.ValidateAsync(command, CancellationToken.None);

        //Assert
        Assert.False(result.IsValid);
        Assert.Single(result.Errors);
        Assert.Equal(nameof(command.UserId), result.Errors.First().PropertyName);
        Assert.Equal(ErrorsResource.Required, result.Errors.First().ErrorMessage);
    }

    [Fact]
    public async Task Validator_Should_ReturnRequiredProjectId_WhenProjectIdIsEmpty()
    {
        //Arrange
        var command = new AddUserToProjectCommand()
        {
            UserId = 2,
        };

        var validator = new AddUserToProjectCommandValidator(
            _projectRepository, _userRepository, _currentUser);

        //Act
        var result = await validator.ValidateAsync(command, CancellationToken.None);

        //Assert
        Assert.False(result.IsValid);
        Assert.Single(result.Errors);
        Assert.Equal(nameof(command.ProjectId), result.Errors.First().PropertyName);
        Assert.Equal(ErrorsResource.Required, result.Errors.First().ErrorMessage);
    }
}
