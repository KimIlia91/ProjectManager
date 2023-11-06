using PM.Application.Common.Resources;
using PM.Application.Features.EmployeeContext.Queries.GetEmployee;
using PM.Test.Common.Constants;
using PM.Test.Common.FakeRepositories;

namespace PM.Test.UserTests.Queries.GetUser;

public class GetUserHandler
{
    private readonly FakeUserRepository _userRepository;

    public GetUserHandler()
    {
        var guid = Guid.NewGuid();
        _userRepository = new FakeUserRepository(guid);
    }

    [Fact]
    public async Task Handler_Should_ReturntGetUserResult_WhenUserInDatabase()
    {
        //Arrange
        var query = new GetUserQuery(1);
        var handler = new GetUserQueryHandler(_userRepository);

        //Act
        var result = await handler.Handle(query, CancellationToken.None);

        //Assert
        Assert.NotNull(result.Value);
        Assert.False(result.IsError);
        Assert.Equal(TestDataConstants.TestUserFirstName, result.Value.FirstName);
        Assert.Equal(TestDataConstants.TestUserLastName, result.Value.LastName);
        Assert.Equal(TestDataConstants.TestUserMiddleName, result.Value.MiddleName);
        Assert.Equal(TestDataConstants.TestUserEmail, result.Value.Email);
    }

    [Fact]
    public async Task Handler_Should_ReturntGetUserResult_WhenUserIsNotInDatabase()
    {
        //Arrange
        var query = new GetUserQuery(110);
        var handler = new GetUserQueryHandler(_userRepository);

        //Act
        var result = await handler.Handle(query, CancellationToken.None);

        //Assert
        Assert.Single(result.Errors);
        Assert.True(result.IsError);
        Assert.Equal(ErrorsResource.NotFound, result.Errors.First().Code);
        Assert.Equal(nameof(query.UserId), result.Errors.First().Description);
    }
}
