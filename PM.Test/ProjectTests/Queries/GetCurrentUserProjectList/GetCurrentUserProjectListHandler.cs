using PM.Application.Features.ProjectContext.Queries.GetCurrentUserProjectList;
using PM.Test.Common.FakeRepositories;

namespace PM.Test.ProjectTests.Queries.GetCurrentUserProjectList;

public sealed class GetCurrentUserProjectListHandler
{
    private readonly FakeProjectRepository _projectRepository;

    public GetCurrentUserProjectListHandler()
    {
        _projectRepository = new FakeProjectRepository();
    }

    [Fact]
    public async Task Handler_Should_ReturnCurrentUserProjectList_WhenProjectOfUserInDatabase()
    {
        //Arrange
        var query = new GetCurrentUserProjectListQuery();
        var handler = new GetCurrentUserProjectListQueryHandler();
    }
}
