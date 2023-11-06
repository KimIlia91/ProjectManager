using PM.Application.Common.Resources;
using PM.Application.Features.EmployeeProjectsContext.Commands.RemoveEmployeeFromProject;
using PM.Test.Common.FakeRepositories;
using PM.Test.Common.FakeServices;
using System;

namespace PM.Test.UserProjectTests.Commands.RemoveUserFromProject;

public sealed class RemoveUserFromProjectValidator
{
    private readonly FakeProjectRepository _projectRepository;
    private readonly FakeUserRepository _userRepository;
    private readonly FakeCurrentUserService _currentUserService;
    private readonly FakeCurrentUserIsSupervisorService _userIsSupervisorService;

    public RemoveUserFromProjectValidator()
    {
        _projectRepository = new FakeProjectRepository();
        _userRepository = new FakeUserRepository();
        _currentUserService = new FakeCurrentUserService();
        _userIsSupervisorService = new FakeCurrentUserIsSupervisorService();
    }

    [Fact]
    public async Task Validator_Should_ReturnNoErrors_WhenAllAreValid()
    {
        //Arrange
        var command = new RemoveUserFromProjectCommand()
        {
            UserId = 2,
            ProjectId = 1,
        };

        var validator = new RemoveUserFromProjectCommandValidator(
            _projectRepository, _userRepository, _currentUserService);

        //Act
        var result = await validator.ValidateAsync(command);

        //Assert
        Assert.True(result.IsValid);
        Assert.Empty(result.Errors);
    }

    [Fact]
    public async Task Validator_Should_ReturnUserIdRequired_WhenUserIdIsEmpty()
    {
        //Arrange
        var command = new RemoveUserFromProjectCommand()
        {
            ProjectId = 1,
        };

        var validator = new RemoveUserFromProjectCommandValidator(
            _projectRepository, _userRepository, _currentUserService);

        //Act
        var result = await validator.ValidateAsync(command);

        //Assert
        Assert.False(result.IsValid);
        Assert.Single(result.Errors);
        Assert.Equal(nameof(command.UserId), result.Errors.First().PropertyName);
        Assert.Equal(ErrorsResource.Required, result.Errors.First().ErrorMessage);
    }

    [Fact]
    public async Task Validator_Should_ReturnUserIdNotFound_WhenUserIsNotInProject()
    {
        //Arrange
        var command = new RemoveUserFromProjectCommand()
        {
            UserId = 3,
            ProjectId = 1,
        };

        var validator = new RemoveUserFromProjectCommandValidator(
            _projectRepository, _userRepository, _currentUserService);

        //Act
        var result = await validator.ValidateAsync(command);

        //Assert
        Assert.False(result.IsValid);
        Assert.Single(result.Errors);
        Assert.Equal(nameof(command.UserId), result.Errors.First().PropertyName);
        Assert.Equal(ErrorsResource.NotFound, result.Errors.First().ErrorMessage);
    }

    [Fact]
    public async Task Validator_Should_ReturnUserIdNotFound_WhenUserIsNotInDatabase()
    {
        //Arrange
        var command = new RemoveUserFromProjectCommand()
        {
            UserId = 300,
            ProjectId = 1,
        };

        var validator = new RemoveUserFromProjectCommandValidator(
            _projectRepository, _userRepository, _currentUserService);

        //Act
        var result = await validator.ValidateAsync(command);

        //Assert
        Assert.False(result.IsValid);
        Assert.Single(result.Errors);
        Assert.Equal(nameof(command.UserId), result.Errors.First().PropertyName);
        Assert.Equal(ErrorsResource.NotFound, result.Errors.First().ErrorMessage);
    }

    [Fact]
    public async Task Validator_Should_ReturnProjectIdRequiredError_WhenProjectIdIsEmpty()
    {
        //Arrange
        var command = new RemoveUserFromProjectCommand()
        {
            UserId = 1,
        };

        var validator = new RemoveUserFromProjectCommandValidator(
            _projectRepository, _userRepository, _currentUserService);

        //Act
        var result = await validator.ValidateAsync(command);

        //Assert
        Assert.False(result.IsValid);
        Assert.Single(result.Errors);
        Assert.Equal(nameof(command.ProjectId), result.Errors.First().PropertyName);
        Assert.Equal(ErrorsResource.Required, result.Errors.First().ErrorMessage);
    }

    [Fact]
    public async Task Validator_Should_ReturnProjectIdNotFoundError_WhenProjectIsNotInDatabase()
    {
        //Arrange
        var command = new RemoveUserFromProjectCommand()
        {
            UserId = 1,
            ProjectId = 100
        };

        var validator = new RemoveUserFromProjectCommandValidator(
            _projectRepository, _userRepository, _currentUserService);

        //Act
        var result = await validator.ValidateAsync(command);

        //Assert
        Assert.False(result.IsValid);
        Assert.Single(result.Errors);
        Assert.Equal(nameof(command.ProjectId), result.Errors.First().PropertyName);
        Assert.Equal(ErrorsResource.NotFound, result.Errors.First().ErrorMessage);
    }

    [Fact]
    public async Task Validator_Should_ReturnProjectIdNotFound_WhenCurrentUserIsNotManagerOfProject()
    {
        //Arrange
        var command = new RemoveUserFromProjectCommand()
        {
            UserId = 3,
            ProjectId = 2
        };

        var validator = new RemoveUserFromProjectCommandValidator(
            _projectRepository, _userRepository, _currentUserService);

        //Act
        var result = await validator.ValidateAsync(command);

        //Assert
        Assert.False(result.IsValid);
        Assert.Single(result.Errors);
        Assert.Equal(nameof(command.ProjectId), result.Errors.First().PropertyName);
        Assert.Equal(ErrorsResource.NotFound, result.Errors.First().ErrorMessage);
    }

    [Fact]
    public async Task Validator_Should_ReturnNoErrors_WhenCurrentUserIsSupervisor()
    {
        //Arrange
        var command = new RemoveUserFromProjectCommand()
        {
            UserId = 2,
            ProjectId = 2
        };

        var validator = new RemoveUserFromProjectCommandValidator(
            _projectRepository, _userRepository, _userIsSupervisorService);

        //Act
        var result = await validator.ValidateAsync(command);

        //Assert
        Assert.True(result.IsValid);
        Assert.Empty(result.Errors);
    }
}
