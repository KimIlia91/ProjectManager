using PM.Application.Features.UserContext.Commands.CreateUser;
using PM.Test.Common.FakeRepositories;
using System;

namespace PM.Test.UserTests.Commands.CreateUser;

public class CreateUserValidator
{
    private readonly FakeUserRepository _userRepository;

    public CreateUserValidator()
    {
        _userRepository = new FakeUserRepository();
    }

    [Fact]
    public async Task Validator_Should_ReturnValidResult_WhenAllValid()
    {
        //Arrange
        var userFirstName = "User 1 FirstName";
        var userLastName = "User 1 LastName";
        var middleName = "User 1";
        var email = "User1@user.com";
        var password = "User123!";

        var command = new CreateUserCommand(
            userFirstName,
            userLastName,
            middleName,
            email,
            password);

        var validator = new CreateUserCommandValidator(_userRepository);

        //Act
        var result = await validator.ValidateAsync(command);

        //Assert
        Assert.True(result.IsValid);
    }

    [Fact]
    public async Task Validator_Should_ReturnValidationEmailError_WhenEmailInvalid()
    {
        //Arrange
        var userFirstName = "User 1 FirstName";
        var userLastName = "User 1 LastName";
        var middleName = "User 1";
        var invalidEmail = "Useruser.com";
        var password = "User123!";

        var command = new CreateUserCommand(
            userFirstName,
            userLastName,
            middleName,
            invalidEmail,
            password);

        var validator = new CreateUserCommandValidator(_userRepository);

        //Act
        var result = await validator.ValidateAsync(command);
        var errorMess = result.Errors.First().ErrorMessage;

        //Assert
        Assert.False(result.IsValid);
        Assert.Equal("Invalid email", errorMess);
    }

    [Fact]
    public async Task Validator_Should_ReturnValidationFirstName_WhenFirstNameNull()
    {
        //Arrange
        string userFirstName = null;
        var userLastName = "User 1 LastName";
        var email = "Useruser.com";

        var command = new CreateUserCommand(
            userFirstName,
            userLastName,
            "User 1",
            email,
            "User123!");

        var validator = new CreateUserCommandValidator(_userRepository);

        //Act
        var result = await validator.ValidateAsync(command);
        var errorMess = result.Errors.First().ErrorMessage;

        //Assert
        Assert.False(result.IsValid);
        Assert.Equal("Required field", errorMess);
    }

    [Fact]
    public async Task Validator_Should_ReturnThreeErrorMessage_WhenFirstNameLastNameEmailInvalids()
    {
        //Arrange
        string userFirstName = null;
        string userLastName = "";
        string userMiddleName = null;
        var email = "Useruser.com";

        var command = new CreateUserCommand(
            userFirstName,
            userLastName,
            userMiddleName,
            email,
            "User123!");

        var validator = new CreateUserCommandValidator(_userRepository);

        //Act
        var result = await validator.ValidateAsync(command);

        //Assert
        Assert.False(result.IsValid);
        Assert.Equal(3, result.Errors.Count);
    }

    [Fact]
    public async Task Validator_Should_ReturnValidResult_WhenMiddleNameNull()
    {
        //Arrange
        var userFirstName = "User 1 FirstName";
        var userLastName = "User 1 LastName";
        string userMiddleName = null;
        var email = "User@user.com";

        var command = new CreateUserCommand(
            userFirstName,
            userLastName,
            userMiddleName,
            email,
            "User123!");

        var validator = new CreateUserCommandValidator(_userRepository);

        //Act
        var result = await validator.ValidateAsync(command);

        //Assert
        Assert.True(result.IsValid);
        Assert.Empty(result.Errors);
    }

    [Fact]
    public async Task Validator_Should_ReturnValidationError_WhenEmailNotUnique()
    {
        //Arrange
        var userFirstName = "User 1 FirstName";
        var userLastName = "User 1 LastName";
        string userMiddleName = null;
        var email = "UserTest@User.com";
        var password = "User123!";

        var command = new CreateUserCommand(
            userFirstName,
            userLastName,
            userMiddleName,
            email,
            password);

        var validator = new CreateUserCommandValidator(_userRepository);

        //Act
        var result = await validator.ValidateAsync(command);
        var emailProperty = result.Errors
           .FirstOrDefault(e => e.PropertyName == nameof(command.Email));

        //Assert
        Assert.False(result.IsValid);
        Assert.NotEmpty(result.Errors);
        Assert.Single(result.Errors);
        Assert.NotNull(emailProperty);
    }

    [Fact]
    public async Task Validator_Should_ReturnPasswordError_WhenPasswordInvalidOnlyLetters()
    {
        //Arrange
        var userFirstName = "User 1 FirstName";
        var userLastName = "User 1 LastName";
        string userMiddleName = null;
        var email = "User1@user.com";
        var invalidPassword = "user123!";

        var command = new CreateUserCommand(
            userFirstName,
            userLastName,
            userMiddleName,
            email,
            invalidPassword);

        var validator = new CreateUserCommandValidator(_userRepository);

        //Act
        var result = await validator.ValidateAsync(command);
        var passwordProperty = result.Errors
            .FirstOrDefault(e => e.PropertyName == nameof(command.Password));

        //Assert
        Assert.False(result.IsValid);
        Assert.NotEmpty(result.Errors);
        Assert.Single(result.Errors);
        Assert.NotNull(passwordProperty);
    }
}
