using Microsoft.EntityFrameworkCore;
using PM.Application.Features.UserContext.Commands.CreateUser;

using PM.Infrastructure.Persistence.Repositories;
using PM.Tests.Common;
using PM.Tests.Common.Interfaces;
using Xunit.Sdk;
using Task = System.Threading.Tasks.Task;

namespace PM.Tests.UserTest.Commads;

public class CreateUserCommandHandlerTests 
{
    private readonly FakeService _service;
    private readonly FakeUserRepository _userRepository;

    public CreateUserCommandHandlerTests()
    {
        _service = new FakeService();
        _userRepository = new FakeUserRepository();

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
        var user = _service.Get().Users.FirstOrDefaultAsync(u => 
            u.Id == result.Value.UserId && 
                u.FirstName == userFirstName && 
                    u.LastName == userLastName && 
                        u.Email == email);

        Assert.NotNull(user);
        Assert.False(result.IsError);
    }

    [Fact]
    public async Task Validator_Should_ReturnValidResult_WhenAllValid()
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
        Assert.Equal(ErrorsResource)
    }
}