using ErrorOr;
using Microsoft.EntityFrameworkCore;
using PM.Application.Features.UserContext.Commands.CreateUser;
using PM.Test.Common.FakeRepositories;
using PM.Domain.Common.Errors;

namespace PM.Test.UserTests.Commands.CreateUser;

public class CreateUserHandler
{
    private readonly FakeIdentityService _service;

    public CreateUserHandler()
    {
        _service = new FakeIdentityService();
    }

    [Fact]
    public async Task Handler_Should_ReturnCreateUserResult_WhenAllValid()
    {
        //Arrange
        var userFirstName = "User 1 FirstName";
        var userLastName = "User 1 LastName";
        var email = "User1@user.com";

        var command = new CreateUserCommand(
            userFirstName,
            userLastName,
            "User 1",
            email,
            "User123!");

        var handler = new CreateUserCommandHandler(_service);

        //Act
        var result = await handler.Handle(command, CancellationToken.None);

        //Assert
        var user = _service.Get().Users
            .FirstOrDefaultAsync(u => u.Id == result.Value.UserId);

        Assert.NotNull(user.Result);
        Assert.False(result.IsError);
        Assert.Equal(result.Value.UserId, user.Result.Id);
    }

    [Fact]
    public async Task Handler_Should_ReturnValidationError_WhenFirstNameNull()
    {
        //Arrange
        string userFirstName = null;
        var userLastName = "User 1 LastName";
        var email = "User1@user.com";

        var command = new CreateUserCommand(
            userFirstName,
            userLastName,
            "User 1",
            email,
            "User123!");

        var handler = new CreateUserCommandHandler(_service);

        //Act
        var result = await handler.Handle(command, CancellationToken.None);

        //Assert
        Assert.True(result.IsError);
        Assert.Equal(Error.Validation().Type, result.FirstError.Type);
        Assert.Equal(Errors.User.FirstNameRequired.Description, result.FirstError.Description);
    }

    [Fact]
    public async Task Handler_Should_ReturnValidationError_WhenLastNameEmpty()
    {
        //Arrange
        string userFirstName = "User 1 FirstName";
        var userLastName = "";
        var email = "User1@user.com";

        var command = new CreateUserCommand(
            userFirstName,
            userLastName,
            "User 1",
            email,
            "User123!");

        var handler = new CreateUserCommandHandler(_service);

        //Act
        var result = await handler.Handle(command, CancellationToken.None);

        //Assert
        Assert.True(result.IsError);
        Assert.Equal(Error.Validation().Type, result.FirstError.Type);
        Assert.Equal(Errors.User.LastNameRequired.Description, result.FirstError.Description);
    }

    [Fact]
    public async Task Handler_Should_ReturnValidationError_WhenEmailNotValid()
    {
        //Arrange
        string userFirstName = "User 1 FirstName";
        var userLastName = "User 1 LastName";
        var email = "Useruser.com";

        var command = new CreateUserCommand(
            userFirstName,
            userLastName,
            "User 1",
            email,
            "User123!");

        var handler = new CreateUserCommandHandler(_service);

        //Act
        var result = await handler.Handle(command, CancellationToken.None);

        //Assert
        Assert.True(result.IsError);
        Assert.Equal(Error.Validation().Type, result.FirstError.Type);
        Assert.Equal(Errors.User.InvalidEmail.Description, result.FirstError.Description);
    }

    [Fact]
    public async Task Handler_Should_ReturnCreateUserResult_WhenMiddleNameNull()
    {
        //Arrange
        string userFirstName = "User 1 FirstName";
        var userLastName = "User 1 LastName";
        string userMiddleName = null;
        var email = "User@user.com";

        var command = new CreateUserCommand(
            userFirstName,
            userLastName,
            userMiddleName,
            email,
            "User123!");

        var handler = new CreateUserCommandHandler(_service);

        //Act
        var result = await handler.Handle(command, CancellationToken.None);

        var user = await _service.Get().Users.FirstOrDefaultAsync(u =>
           u.Id == result.Value.UserId &&
               u.FirstName == userFirstName &&
                   u.LastName == userLastName &&
                       u.Email == email);

        //Assert
        Assert.False(result.IsError);
        Assert.NotNull(user);
        Assert.Equal(result.Value.UserId, user.Id);
    }
}
