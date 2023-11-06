using PM.Application.Common.Resources;
using PM.Application.Features.ProjectContext.Commands.CreateProject;
using PM.Domain.Common.Enums;
using PM.Test.Common.FakeRepositories;

namespace PM.Test.ProjectTests.Commands.CreateProject;

public class CreateProjectValidator
{
    private readonly FakeUserRepository _userRepository;

    public CreateProjectValidator()
    {
        _userRepository = new FakeUserRepository();
    }

    [Fact]
    public async Task Validator_Should_ReturnNoErrorsIsValid_WhenAllAreValid()
    {
        //Arrange
        var command = new CreateProjectCommand()
        {
            Name = "Name",
            CustomerCompany = "Customer Company",
            ExecutorCompany = "Executor Company",
            ManagerId = 2,
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddMonths(1),
            Priority = Priority.Low
        };

        var validator = new CreateProjectCommandValidator(_userRepository);

        //Act
        var result = await validator.ValidateAsync(command, CancellationToken.None);

        //Assert
        Assert.True(result.IsValid);
    }

    [Fact]
    public async Task Validator_Should_ReturnNameError_WhenNameEmpty()
    {
        //Arrange
        var command = new CreateProjectCommand()
        {
            Name = string.Empty,
            CustomerCompany = "Customer Company",
            ExecutorCompany = "Executor Company",
            ManagerId = 2,
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddMonths(1),
            Priority = Priority.Low
        };

        var validator = new CreateProjectCommandValidator(_userRepository);

        //Act
        var result = await validator.ValidateAsync(command, CancellationToken.None);

        //Assert
        Assert.False(result.IsValid);
        Assert.Equal(ErrorsResource.Required, result.Errors.First().ErrorMessage);
        Assert.Equal(nameof(command.Name), result.Errors.First().PropertyName);
    }

    [Fact]
    public async Task Validator_Should_ReturnNameError_WhenNameIsNull()
    {
        //Arrange
        var command = new CreateProjectCommand()
        {
            Name = null,
            CustomerCompany = "Customer Company",
            ExecutorCompany = "Executor Company",
            ManagerId = 2,
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddMonths(1),
            Priority = Priority.Low
        };

        var validator = new CreateProjectCommandValidator(_userRepository);

        //Act
        var result = await validator.ValidateAsync(command, CancellationToken.None);

        //Assert
        Assert.False(result.IsValid);
        Assert.Equal(ErrorsResource.Required, result.Errors.First().ErrorMessage);
        Assert.Equal(nameof(command.Name), result.Errors.First().PropertyName);
    }

    [Fact]
    public async Task Validator_Should_ReturnCustomerCompanyError_WhenNameIsEmpty()
    {
        //Arrange
        var command = new CreateProjectCommand()
        {
            Name = "Name",
            CustomerCompany = string.Empty,
            ExecutorCompany = "Executor Company",
            ManagerId = 2,
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddMonths(1),
            Priority = Priority.Low
        };

        var validator = new CreateProjectCommandValidator(_userRepository);

        //Act
        var result = await validator.ValidateAsync(command, CancellationToken.None);

        //Assert
        Assert.False(result.IsValid);
        Assert.Equal(ErrorsResource.Required, result.Errors.First().ErrorMessage);
        Assert.Equal(nameof(command.CustomerCompany), result.Errors.First().PropertyName);
    }

    [Fact]
    public async Task Validator_Should_ReturnCustomerCompanyError_WhenNameIsNull()
    {
        //Arrange
        var command = new CreateProjectCommand()
        {
            Name = "Name",
            CustomerCompany = null,
            ExecutorCompany = "Executor Company",
            ManagerId = 2,
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddMonths(1),
            Priority = Priority.Low
        };

        var validator = new CreateProjectCommandValidator(_userRepository);

        //Act
        var result = await validator.ValidateAsync(command, CancellationToken.None);

        //Assert
        Assert.False(result.IsValid);
        Assert.Equal(ErrorsResource.Required, result.Errors.First().ErrorMessage);
        Assert.Equal(nameof(command.CustomerCompany), result.Errors.First().PropertyName);
    }

    [Fact]
    public async Task Validator_Should_ReturnExecutorCompanyError_WhenNameIsEmpty()
    {
        //Arrange
        var command = new CreateProjectCommand()
        {
            Name = "Name",
            CustomerCompany = "Customer Company",
            ExecutorCompany = string.Empty,
            ManagerId = 2,
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddMonths(1),
            Priority = Priority.Low
        };

        var validator = new CreateProjectCommandValidator(_userRepository);

        //Act
        var result = await validator.ValidateAsync(command, CancellationToken.None);

        //Assert
        Assert.False(result.IsValid);
        Assert.Equal(ErrorsResource.Required, result.Errors.First().ErrorMessage);
        Assert.Equal(nameof(command.ExecutorCompany), result.Errors.First().PropertyName);
    }

    [Fact]
    public async Task Validator_Should_ReturnExecutorCompanyError_WhenNameIsNull()
    {
        //Arrange
        var command = new CreateProjectCommand()
        {
            Name = "Name",
            CustomerCompany = "Customer Company",
            ExecutorCompany = null,
            ManagerId = 2,
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddMonths(1),
            Priority = Priority.Low
        };

        var validator = new CreateProjectCommandValidator(_userRepository);

        //Act
        var result = await validator.ValidateAsync(command, CancellationToken.None);

        //Assert
        Assert.False(result.IsValid);
        Assert.Equal(ErrorsResource.Required, result.Errors.First().ErrorMessage);
        Assert.Equal(nameof(command.ExecutorCompany), result.Errors.First().PropertyName);
    }

    [Fact]
    public async Task Validator_Should_ReturnManagerIdError_WhenManagerNotFound()
    {
        //Arrange
        var command = new CreateProjectCommand()
        {
            Name = "Name",
            CustomerCompany = "Customer Company",
            ExecutorCompany = "Executor Company",
            ManagerId = 200,
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddMonths(1),
            Priority = Priority.Low
        };

        var validator = new CreateProjectCommandValidator(_userRepository);

        //Act
        var result = await validator.ValidateAsync(command, CancellationToken.None);

        //Assert
        Assert.False(result.IsValid);
        Assert.Equal(ErrorsResource.NotFound, result.Errors.First().ErrorMessage);
        Assert.Equal(nameof(command.ManagerId), result.Errors.First().PropertyName);
    }

    [Fact]
    public async Task Validator_Should_ReturnManagerIdError_WhenManagerIdIsEmpty()
    {
        //Arrange
        var command = new CreateProjectCommand()
        {
            Name = "Name",
            CustomerCompany = "Customer Company",
            ExecutorCompany = "Executor Company",
            ManagerId = 0,
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddMonths(1),
            Priority = Priority.Low
        };

        var validator = new CreateProjectCommandValidator(_userRepository);

        //Act
        var result = await validator.ValidateAsync(command, CancellationToken.None);

        //Assert
        Assert.False(result.IsValid);
        Assert.Equal(ErrorsResource.Required, result.Errors.First().ErrorMessage);
        Assert.Equal(nameof(command.ManagerId), result.Errors.First().PropertyName);
    }

    [Fact]
    public async Task Validator_Should_ReturnDateError_WhenStartDateMoreThanEndDate()
    {
        //Arrange
        var command = new CreateProjectCommand()
        {
            Name = "Name",
            CustomerCompany = "Customer Company",
            ExecutorCompany = "Executor Company",
            ManagerId = 2,
            StartDate = DateTime.UtcNow.AddMonths(1),
            EndDate = DateTime.UtcNow,
            Priority = Priority.Low
        };

        var validator = new CreateProjectCommandValidator(_userRepository);

        //Act
        var result = await validator.ValidateAsync(command, CancellationToken.None);

        //Assert
        Assert.False(result.IsValid);
        Assert.Equal(2, result.Errors.Count);
        Assert.Equal(ErrorsResource.InvalidDate, result.Errors
            .First(e => e.PropertyName == nameof(command.StartDate)).ErrorMessage);
        Assert.Equal(ErrorsResource.InvalidDate, result.Errors
            .First(e => e.PropertyName == nameof(command.EndDate)).ErrorMessage);
    }

    [Fact]
    public async Task Validator_Should_ReturnDateError_WhenEndDateIsEmpty()
    {
        //Arrange
        var command = new CreateProjectCommand()
        {
            Name = "Name",
            CustomerCompany = "Customer Company",
            ExecutorCompany = "Executor Company",
            ManagerId = 2,
            StartDate = DateTime.UtcNow,
            Priority = Priority.Low
        };

        var validator = new CreateProjectCommandValidator(_userRepository);

        //Act
        var result = await validator.ValidateAsync(command, CancellationToken.None);

        //Assert
        Assert.False(result.IsValid);
        Assert.Equal(ErrorsResource.Required, result.Errors
            .First(e => e.PropertyName == nameof(command.EndDate)).ErrorMessage);
    }

    [Fact]
    public async Task Validator_Should_ReturnDateError_WhenStartDateIsEmpty()
    {
        //Arrange
        var command = new CreateProjectCommand()
        {
            Name = "Name",
            CustomerCompany = "Customer Company",
            ExecutorCompany = "Executor Company",
            ManagerId = 2,
            EndDate = DateTime.UtcNow,
            Priority = Priority.Low
        };

        var validator = new CreateProjectCommandValidator(_userRepository);

        //Act
        var result = await validator.ValidateAsync(command, CancellationToken.None);

        //Assert
        Assert.False(result.IsValid);
        Assert.Equal(ErrorsResource.Required, result.Errors
            .First(e => e.PropertyName == nameof(command.StartDate)).ErrorMessage);
    }
}
