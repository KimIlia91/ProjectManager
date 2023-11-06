using PM.Application.Common.Resources;
using PM.Application.Features.ProjectContext.Commands.UpdateProject;
using PM.Test.Common.FakeRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PM.Test.ProjectTests.Commands.UpdateProject;

public sealed class UpdateProjectValidator
{
    private readonly FakeProjectRepository _projectRepository;
    private readonly FakeUserRepository _userRepository;

    public UpdateProjectValidator()
    {
        _projectRepository = new FakeProjectRepository();
        _userRepository = new FakeUserRepository();
    }

    [Fact]
    public async Task Validator_Should_ReturnNoErrors_WhenAllAreValid()
    {
        //Arrange
        var command = new UpdateProjectCommand()
        {
            Id = 1,
            Name = "Test",
            CustomerCompany = "Test",
            ExecutorCompany = "Test",
            ManagerId = 2,
            StartDate = DateTime.UtcNow.AddDays(4),
            EndDate = DateTime.UtcNow.AddDays(4),
            Priority = Domain.Common.Enums.Priority.High
        };

        var validator = new UpdateProjectCommandValidator(_projectRepository, _userRepository);

        //Act
        var result = await validator.ValidateAsync(command, CancellationToken.None);

        //Assert
        Assert.True(result.IsValid);
    }

    [Fact]
    public async Task Validator_Should_ReturnProjectIdError_WhenProjectNotFound()
    {
        //Arrange
        var command = new UpdateProjectCommand()
        {
            Id = 112,
            Name = "Test",
            CustomerCompany = "Test",
            ExecutorCompany = "Test",
            ManagerId = 2,
            StartDate = DateTime.UtcNow.AddDays(4),
            EndDate = DateTime.UtcNow.AddDays(4),
            Priority = Domain.Common.Enums.Priority.High
        };

        var validator = new UpdateProjectCommandValidator(_projectRepository, _userRepository);

        //Act
        var result = await validator.ValidateAsync(command, CancellationToken.None);

        //Assert
        Assert.False(result.IsValid);
        Assert.Single(result.Errors);
        Assert.Equal(ErrorsResource.NotFound, result.Errors.First().ErrorMessage);
    }

    [Fact]
    public async Task Validator_Should_ReturnNameError_WhenNameIsEmpty()
    {
        //Arrange
        var command = new UpdateProjectCommand()
        {
            Id = 1,
            Name = string.Empty,
            CustomerCompany = "Test",
            ExecutorCompany = "Test",
            ManagerId = 2,
            StartDate = DateTime.UtcNow.AddDays(4),
            EndDate = DateTime.UtcNow.AddDays(4),
            Priority = Domain.Common.Enums.Priority.High
        };

        var validator = new UpdateProjectCommandValidator(_projectRepository, _userRepository);

        //Act
        var result = await validator.ValidateAsync(command, CancellationToken.None);

        //Assert
        Assert.False(result.IsValid);
        Assert.Single(result.Errors);
        Assert.Equal(ErrorsResource.Required, result.Errors.First().ErrorMessage);
    }

    [Fact]
    public async Task Validator_Should_ReturnCustomerCompanyError_WhenCustomerCompanyIsEmpty()
    {
        //Arrange
        var command = new UpdateProjectCommand()
        {
            Id = 1,
            Name = "Test",
            CustomerCompany = string.Empty,
            ExecutorCompany = "Test",
            ManagerId = 2,
            StartDate = DateTime.UtcNow.AddDays(4),
            EndDate = DateTime.UtcNow.AddDays(4),
            Priority = Domain.Common.Enums.Priority.High
        };

        var validator = new UpdateProjectCommandValidator(_projectRepository, _userRepository);

        //Act
        var result = await validator.ValidateAsync(command, CancellationToken.None);

        //Assert
        Assert.False(result.IsValid);
        Assert.Single(result.Errors);
        Assert.Equal(ErrorsResource.Required, result.Errors.First().ErrorMessage);
    }

    [Fact]
    public async Task Validator_Should_ReturnExecutorCompanyError_WhenExecutorCompanyIsEmpty()
    {
        //Arrange
        var command = new UpdateProjectCommand()
        {
            Id = 1,
            Name = "Test",
            CustomerCompany = "Test",
            ExecutorCompany = string.Empty,
            ManagerId = 2,
            StartDate = DateTime.UtcNow.AddDays(4),
            EndDate = DateTime.UtcNow.AddDays(4),
            Priority = Domain.Common.Enums.Priority.High
        };

        var validator = new UpdateProjectCommandValidator(_projectRepository, _userRepository);

        //Act
        var result = await validator.ValidateAsync(command, CancellationToken.None);

        //Assert
        Assert.False(result.IsValid);
        Assert.Single(result.Errors);
        Assert.Equal(ErrorsResource.Required, result.Errors.First().ErrorMessage);
    }

    [Fact]
    public async Task Validator_Should_ReturnManagerIdError_WhenManagerIdNotFound()
    {
        //Arrange
        var command = new UpdateProjectCommand()
        {
            Id = 1,
            Name = "Test",
            CustomerCompany = "Test",
            ExecutorCompany = "Test",
            ManagerId = 222,
            StartDate = DateTime.UtcNow.AddDays(4),
            EndDate = DateTime.UtcNow.AddDays(4),
            Priority = Domain.Common.Enums.Priority.High
        };

        var validator = new UpdateProjectCommandValidator(_projectRepository, _userRepository);

        //Act
        var result = await validator.ValidateAsync(command, CancellationToken.None);

        //Assert
        Assert.False(result.IsValid);
        Assert.Single(result.Errors);
        Assert.Equal(ErrorsResource.NotFound, result.Errors.First().ErrorMessage);
    }

    [Fact]
    public async Task Validator_Should_ReturnManagerIdError_WhenManagerIdIsEmpty()
    {
        //Arrange
        var command = new UpdateProjectCommand()
        {
            Id = 1,
            Name = "Test",
            CustomerCompany = "Test",
            ExecutorCompany = "Test",
            StartDate = DateTime.UtcNow.AddDays(4),
            EndDate = DateTime.UtcNow.AddDays(4),
            Priority = Domain.Common.Enums.Priority.High
        };

        var validator = new UpdateProjectCommandValidator(_projectRepository, _userRepository);

        //Act
        var result = await validator.ValidateAsync(command, CancellationToken.None);

        //Assert
        Assert.False(result.IsValid);
        Assert.Single(result.Errors);
        Assert.Equal(ErrorsResource.Required, result.Errors.First().ErrorMessage);
    }

    [Fact]
    public async Task Validator_Should_ReturnDateError_WhenStartDateMoreThanEndDate()
    {
        //Arrange
        var command = new UpdateProjectCommand()
        {
            Id = 1,
            Name = "Test",
            CustomerCompany = "Test",
            ExecutorCompany = "Test",
            ManagerId = 2,
            StartDate = DateTime.UtcNow.AddDays(4),
            EndDate = DateTime.UtcNow.AddDays(2),
            Priority = Domain.Common.Enums.Priority.High
        };

        var validator = new UpdateProjectCommandValidator(_projectRepository, _userRepository);

        //Act
        var result = await validator.ValidateAsync(command, CancellationToken.None);

        //Assert
        Assert.False(result.IsValid);
        Assert.Single(result.Errors);
        Assert.Equal(ErrorsResource.InvalidDate,
            result.Errors.First(e => e.PropertyName == nameof(command.EndDate)).ErrorMessage);
    }

    [Fact]
    public async Task Validator_Should_ReturnDateError_WhenStartDateIsEmpty()
    {
        //Arrange
        var command = new UpdateProjectCommand()
        {
            Id = 1,
            Name = "Test",
            CustomerCompany = "Test",
            ExecutorCompany = "Test",
            ManagerId = 2,
            EndDate = DateTime.UtcNow.AddDays(2),
            Priority = Domain.Common.Enums.Priority.High
        };

        var validator = new UpdateProjectCommandValidator(_projectRepository, _userRepository);

        //Act
        var result = await validator.ValidateAsync(command, CancellationToken.None);

        //Assert
        Assert.False(result.IsValid);
        Assert.Single(result.Errors);
        Assert.Equal(ErrorsResource.Required,
            result.Errors.First(e => e.PropertyName == nameof(command.StartDate)).ErrorMessage);
    }

    [Fact]
    public async Task Validator_Should_ReturnDateError_WhenEndDateIsEmpty()
    {
        //Arrange
        var command = new UpdateProjectCommand()
        {
            Id = 1,
            Name = "Test",
            CustomerCompany = "Test",
            ExecutorCompany = "Test",
            ManagerId = 2,
            StartDate = DateTime.UtcNow.AddDays(2),
            Priority = Domain.Common.Enums.Priority.High
        };

        var validator = new UpdateProjectCommandValidator(_projectRepository, _userRepository);

        //Act
        var result = await validator.ValidateAsync(command, CancellationToken.None);

        //Assert
        Assert.False(result.IsValid);
        Assert.Single(result.Errors);
        Assert.Equal(ErrorsResource.Required,
            result.Errors.First(e => e.PropertyName == nameof(command.EndDate)).ErrorMessage);
    }

    [Fact]
    public async Task Validator_Should_ReturnErrorMessages_WhenAllFieldIsInvalid()
    {
        //Arrange
        var command = new UpdateProjectCommand();

        var validator = new UpdateProjectCommandValidator(_projectRepository, _userRepository);

        //Act
        var result = await validator.ValidateAsync(command, CancellationToken.None);

        //Assert
        Assert.False(result.IsValid);
        Assert.Equal(8, result.Errors.Count);
        Assert.Equal(ErrorsResource.Required,
            result.Errors.First(e => e.PropertyName == nameof(command.Id)).ErrorMessage);

        Assert.Equal(ErrorsResource.Required,
            result.Errors.First(e => e.PropertyName == nameof(command.Name)).ErrorMessage);

        Assert.Equal(ErrorsResource.Required,
            result.Errors.First(e => e.PropertyName == nameof(command.CustomerCompany)).ErrorMessage);

        Assert.Equal(ErrorsResource.Required,
            result.Errors.First(e => e.PropertyName == nameof(command.ExecutorCompany)).ErrorMessage);

        Assert.Equal(ErrorsResource.Required,
            result.Errors.First(e => e.PropertyName == nameof(command.ManagerId)).ErrorMessage);

        Assert.Equal(ErrorsResource.Required,
            result.Errors.First(e => e.PropertyName == nameof(command.EndDate)).ErrorMessage);

        Assert.Equal(ErrorsResource.Required,
            result.Errors.First(e => e.PropertyName == nameof(command.Priority)).ErrorMessage);

        Assert.Equal(ErrorsResource.Required,
            result.Errors.Last(e => e.PropertyName == nameof(command.StartDate)).ErrorMessage);
    }
}
