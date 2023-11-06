using PM.Application.Common.Resources;
using PM.Application.Features.ProjectContext.Queries.GetProject;
using PM.Test.Common.FakeRepositories;

namespace PM.Test.ProjectTests.Queries.GetProject;

public class GetProjectHandler
{
    private readonly FakeProjectRepository _projectRepository;

    public GetProjectHandler()
    {
        var guid = Guid.NewGuid();
        _projectRepository = new FakeProjectRepository(guid);
    }

    [Fact]
    public async Task Handler_Should_ReturnProjectResult_WhenProjectInDatabase()
    {
        //Arrange
        var query = new GetProjectQuery(1);
        var handler = new GetProjectQueryHandler(_projectRepository);

        //Act
        var result = await handler.Handle(query, CancellationToken.None);

        //Assert
        Assert.False(result.IsError);
        Assert.Equal(query.Id, result.Value.Id);
    }


    [Fact]
    public async Task Handler_Should_ReturnNotFoundError_WhenProjectIsNotInDatabase()
    {
        //Arrange
        var query = new GetProjectQuery(101);
        var handler = new GetProjectQueryHandler(_projectRepository);

        //Act
        var result = await handler.Handle(query, CancellationToken.None);

        //Assert
        Assert.True(result.IsError);
        Assert.Equal(ErrorsResource.NotFound, result.Errors.First().Code);
        Assert.Equal(nameof(query.Id), result.Errors.First().Description);
    }
}
