using PM.Application.Features.UserContext.Queries.GetProjectUserList;
using PM.Test.Common.FakeRepositories;

namespace PM.Test.UserTests.Queries.GetProjectUserList;

public class GetProjectUserListHandler
{
    private readonly FakeUserRepository _userRepository;

    public GetProjectUserListHandler()
    {
        _userRepository = new FakeUserRepository();
    }

    [Fact]
    public async Task Handler_Should_ReturnUserResultListOfProject_WhenUserInDatabase()
    {
        //Arrange
        var query = new GetProjectUserListQuery(1);
        var handler = new GetProjectUserListQueryHandler(_userRepository);

        //Act
        var result = await handler.Handle(query, CancellationToken.None);

        //Assert
        Assert.NotEmpty(result.Value);
        Assert.Equal(2, result.Value.Count);
    }

    [Fact]
    public async Task Handler_Should_ReturnEmptyUserList_WhenProjectIsNotInDatabase()
    {
        //Arrange
        var query = new GetProjectUserListQuery(100);
        var handler = new GetProjectUserListQueryHandler(_userRepository);

        //Act
        var result = await handler.Handle(query, CancellationToken.None);

        //Assert
        Assert.Empty(result.Value);
    }
}
