using PM.Application.Features.ProjectContext.Queries.GetProjectList;
using PM.Test.Common.FakeRepositories;

namespace PM.Test.ProjectTests.Queries.GetProjectList;

public sealed class GetProjectListHandler
{
    private readonly FakeProjectRepository _projectRepository;

    public GetProjectListHandler()
    {
        _projectRepository = new FakeProjectRepository();
    }

    [Fact]
    public async Task Handler_Should_ReturnProjectListResult_WhenProjectsInDatabase()
    {
        //Arrange
        var query = new GetProjectListQuery();
        var handler = new GetProjectListQueryHandler(_projectRepository);

        //Act 
        var result = await handler.Handle(query, CancellationToken.None);

        //Assert
        Assert.False(result.IsError);
        Assert.NotEmpty(result.Value);
    }
}
