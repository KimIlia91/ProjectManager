using PM.Application.Features.UserContext.Queries.GetUserList;
using PM.Test.Common.FakeRepositories;
using System;

namespace PM.Test.UserTests.Queries.GetUserList;

public class GetUserListHandler
{
    private readonly FakeUserRepository _userRepository;

    public GetUserListHandler()
    {
        _userRepository = new FakeUserRepository();
    }

    [Fact]
    public async Task Handler_Should_ReturnGetUserList_WhenUserInDb()
    {
        //Arrange
        var query = new GetUserListQuery();
        var handler = new GetUserListQueryHandler(_userRepository);

        //Act
        var result = await handler.Handle(query, CancellationToken.None);

        //Assert
        Assert.False(result.IsError);
        Assert.NotEmpty(result.Value);
    }
}
