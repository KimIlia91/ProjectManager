using PM.Application.Features.ProjectContext.Queries.GetCurrentUserProjectList;
using PM.Test.Common.Constants;
using PM.Test.Common.FakeRepositories;
using PM.Test.Common.FakeServices;

namespace PM.Test.ProjectTests.Queries.GetCurrentUserProjectList;

public sealed class GetCurrentUserProjectListHandler
{
    private readonly FakeProjectRepository _projectRepository;
    private readonly FakeCurrentUserService _userService;

    public GetCurrentUserProjectListHandler()
    {
        var guid = Guid.NewGuid();
        _projectRepository = new FakeProjectRepository(guid);
        _userService = new FakeCurrentUserService();
    }

    [Fact]
    public async Task Handler_Should_ReturnCurrentUserProjectList_WhenProjectsOfUserInDatabase()
    {
        //Arrange
        var query = new GetCurrentUserProjectListQuery();
        var handler = new GetCurrentUserProjectListQueryHandler(
            _userService, _projectRepository);

        //Act
        var result = await handler.Handle(query, CancellationToken.None);

        //Assert
        Assert.False(result.IsError);
        Assert.NotEmpty(result.Value);
        Assert.Single(result.Value);
        Assert.Equal(_userService.UserId, result.Value.First().Manager.Id);
    }

    [Fact]
    public async Task Handler_Should_ReturnCurrentUserEmptyProjectList_WhenProjectsOfUserAreNotInDatabase()
    {
        //Arrange
        var userProjectToDelete = await _projectRepository
            .GetOrDeafaultAsync(p => p.Id == TestDataConstants.TestProjectId, CancellationToken.None);

        await _projectRepository.RemoveAsync(userProjectToDelete, CancellationToken.None);

        var query = new GetCurrentUserProjectListQuery();
        var handler = new GetCurrentUserProjectListQueryHandler(
            _userService, _projectRepository);

        //Act
        var result = await handler.Handle(query, CancellationToken.None);

        //Assert
        Assert.False(result.IsError);
        Assert.Empty(result.Value);
    }
}
