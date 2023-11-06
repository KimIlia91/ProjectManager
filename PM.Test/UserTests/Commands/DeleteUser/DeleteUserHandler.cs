using PM.Application.Common.Interfaces.IRepositories;
using PM.Application.Features.UserContext.Commands.DeleteUser;
using PM.Test.Common.Constants;
using PM.Test.Common.FakeRepositories;

namespace PM.Test.UserTests.Commands.DeleteUser;

public class DeleteUserHandler
{
    private readonly FakeTaskRepository _taskRepository;
    private readonly FakeUserRepository _userRepository;

    public DeleteUserHandler()
    {
        _taskRepository = new FakeTaskRepository();
        _userRepository = new FakeUserRepository();
    }

    [Fact]
    public async Task Validator_Should_ReturnValidResult_WhenValid()
    {
        //Arrange
        var user = await _userRepository
            .GetOrDeafaultAsync(u => u.Email == TestDataConstants.TestUserEmail, CancellationToken.None);

        var command = new DeleteUserCommand() { UserId = user.Id, User = user };
        var handler = new DeleteUserCommandHandler(_userRepository, _taskRepository);

        //Act
        var result = await handler.Handle(command, CancellationToken.None);
        var expectUser = await _userRepository
            .GetOrDeafaultAsync(u => u.Email == TestDataConstants.TestUserEmail, CancellationToken.None);


        //Assert
        Assert.False(result.IsError);
        Assert.Equal(user.Id, result.Value.Id);
        Assert.Null(expectUser);
    }
}
