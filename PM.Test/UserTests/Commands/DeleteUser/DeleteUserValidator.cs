using PM.Application.Common.Resources;
using PM.Application.Features.UserContext.Commands.CreateUser;
using PM.Application.Features.UserContext.Commands.DeleteUser;
using PM.Test.Common.Constants;
using PM.Test.Common.FakeRepositories;

namespace PM.Test.UserTests.Commands.DeleteUser;

public class DeleteUserValidator
{
    private readonly FakeUserRepository _userRepository;

    public DeleteUserValidator()
    {
        _userRepository = new FakeUserRepository();
    }

    [Fact]
    public async Task Validator_Should_ReturnValidationError_WhenNotFoudUser()
    {
        //Arrange
        var command = new DeleteUserCommand() { UserId = 110 };
        var validator = new DeleteUserCommandValidator(_userRepository);

        //Act
        var result = await validator.ValidateAsync(command, CancellationToken.None);

        //Assert
        Assert.False(result.IsValid);
        Assert.Equal(nameof(command.UserId), result.Errors.First().PropertyName);
        Assert.Equal(ErrorsResource.NotFound, result.Errors.First().ErrorMessage);
    }

    [Fact]
    public async Task Validator_Should_ReturnValidationError_WhenIdIsEmpty()
    {
        //Arrange
        var command = new DeleteUserCommand();
        var validator = new DeleteUserCommandValidator(_userRepository);

        //Act
        var result = await validator.ValidateAsync(command, CancellationToken.None);

        //Assert
        Assert.False(result.IsValid);
        Assert.Equal(nameof(command.UserId), result.Errors.First().PropertyName);
        Assert.Equal(ErrorsResource.NotFound, result.Errors.First().ErrorMessage);
    }
}
