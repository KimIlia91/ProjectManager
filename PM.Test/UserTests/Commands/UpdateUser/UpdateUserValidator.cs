using MapsterMapper;
using PM.Application.Features.UserContext.Commands.CreateUser;
using PM.Application.Features.UserContext.Commands.UpdateUser;
using PM.Test.Common.Constants;
using PM.Test.Common.FakeRepositories;

namespace PM.Test.UserTests.Commands.UpdateUser;

public class UpdateUserValidator
{
    private readonly FakeUserRepository _userRepository;
    private readonly string _testUserEmail;

    public UpdateUserValidator()
    {
        _userRepository = new FakeUserRepository();
        _testUserEmail = TestDataConstants.TestUserEmail;
    }

    [Fact]
    public async Task Handler_Should_ReturnDeleteUserResult_WhenValid()
    {
        //Arrange
        var firstName = Guid.NewGuid().ToString();
        var lastName = Guid.NewGuid().ToString();
        var email = "newEmail@user.com";
        var middleName = Guid.NewGuid().ToString();

        var user = await _userRepository
            .GetOrDeafaultAsync(u => u.Email == _testUserEmail, CancellationToken.None);

        var command = new UpdateUserCommand()
        {
            Id = user.Id,
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            MiddelName = middleName,
        };

        var validator = new UpdateUserCommandValidator(_userRepository);

        //Act
        var result = await validator.ValidateAsync(command);

        //Assert
        Assert.True(result.IsValid);
    }

    [Fact]
    public async Task Validator_Should_ReturnValidResult_WhenNoChanges()
    {
        //Arrange
        var user = await _userRepository
            .GetOrDeafaultAsync(u => u.Email == _testUserEmail, CancellationToken.None);

        var command = new UpdateUserCommand()
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            MiddelName = user.MiddleName,
        };

        var validator = new UpdateUserCommandValidator(_userRepository);

        //Act
        var result = await validator.ValidateAsync(command);

        //Assert
        Assert.True(result.IsValid);
    }

    [Fact]
    public async Task Validator_Should_ReturnValidResult_WhenMiddleNameIsNull()
    {
        //Arrange
        var user = await _userRepository
            .GetOrDeafaultAsync(u => u.Email == _testUserEmail, CancellationToken.None);

        var command = new UpdateUserCommand()
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
        };

        var validator = new UpdateUserCommandValidator(_userRepository);

        //Act
        var result = await validator.ValidateAsync(command);

        //Assert
        Assert.True(result.IsValid);
    }

    [Fact]
    public async Task Validator_Should_ReturnFirstNameError_WhenFirstNameIsEmpty()
    {
        //Arrange
        var firstName = string.Empty;
        var lastName = Guid.NewGuid().ToString();
        var email = "newEmail@user.com";
        var middleName = Guid.NewGuid().ToString();

        var user = await _userRepository
            .GetOrDeafaultAsync(u => u.Email == _testUserEmail, CancellationToken.None);

        var command = new UpdateUserCommand()
        {
            Id = user.Id,
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            MiddelName = middleName,
        };

        var validator = new UpdateUserCommandValidator(_userRepository);

        //Act
        var result = await validator.ValidateAsync(command);

        //Assert
        Assert.False(result.IsValid);
        Assert.Equal(nameof(command.FirstName), result.Errors.First().PropertyName);
    }

    [Fact]
    public async Task Validator_Should_ReturnFourErrors_WhenAllIsEmpty()
    {
        //Arrange
        var firstName = string.Empty;
        var lastName = string.Empty;
        var email = string.Empty;
        var middleName = string.Empty;

        var user = await _userRepository
            .GetOrDeafaultAsync(u => u.Email == _testUserEmail, CancellationToken.None);

        var command = new UpdateUserCommand()
        {
            Id = user.Id,
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            MiddelName = middleName,
        };

        var validator = new UpdateUserCommandValidator(_userRepository);

        //Act
        var result = await validator.ValidateAsync(command);

        var actualFirstName = result.Errors
            .First(e => e.PropertyName == nameof(command.FirstName)).PropertyName;

        var actualLastName = result.Errors
            .First(e => e.PropertyName == nameof(command.LastName)).PropertyName;

        var actualEmail = result.Errors
            .First(e => e.PropertyName == nameof(command.Email)).PropertyName;

        //Assert
        Assert.False(result.IsValid);
        Assert.Equal(4, result.Errors.Count);
        Assert.NotNull(actualFirstName);
        Assert.NotNull(actualLastName);
        Assert.NotNull(actualEmail);
    }

    [Fact]
    public async Task Validator_Should_ReturnEmailError_WhenEmailNotUnique()
    {
        //Arrange
        var firstName = Guid.NewGuid().ToString();
        var lastName = Guid.NewGuid().ToString();
        var email = $"2{TestDataConstants.TestUserEmail}";
        var middleName = Guid.NewGuid().ToString();

        var user = await _userRepository
            .GetOrDeafaultAsync(u => u.Email == _testUserEmail, CancellationToken.None);

        var command = new UpdateUserCommand()
        {
            Id = user.Id,
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            MiddelName = middleName,
        };

        var validator = new UpdateUserCommandValidator(_userRepository);

        //Act
        var result = await validator.ValidateAsync(command);
        var actualEmail = result.Errors
         .First(e => e.PropertyName == nameof(command.Email)).PropertyName;

        //Assert
        Assert.False(result.IsValid);
        Assert.NotNull(actualEmail);
    }
}
