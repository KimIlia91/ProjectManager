using Microsoft.EntityFrameworkCore;
using PM.Application.Features.ProjectContext.Commands.DeleteProject;
using PM.Test.Common.FakeRepositories;

namespace PM.Test.ProjectTests.Commands.DeleteProject;

public sealed class DeleteProjectHandler
{
    private readonly FakeProjectRepository _projectRepository;

    public DeleteProjectHandler()
    {
        _projectRepository = new FakeProjectRepository();
    }

    [Fact]
    public async Task Handler_Should_ReturnDeleteProjectResult_WhenAllAreValid()
    {
        //Arrange
        var project = await _projectRepository
            .GetOrDeafaultAsync(p => p.Id == 1, CancellationToken.None);

        var command = new DeleteProjectCommand()
        {
            Project = project,
            Id = project.Id
        };

        var handler = new DeleteProjectCommandHandler(_projectRepository);

        //Act
        var result = await handler.Handle(command, CancellationToken.None);

        //Assert
        Assert.False(result.IsError);
        Assert.Equal(command.Project.Id, result.Value.Id);
    }

    [Fact]
    public async Task Handler_Should_ThrowNullOperationException_WhenPrjectNotFound()
    {
        //Arrange
        var command = new DeleteProjectCommand();

        var handler = new DeleteProjectCommandHandler(_projectRepository);

        // Act
        var exception = await Record.ExceptionAsync(() => 
            handler.Handle(command, CancellationToken.None));

        // Assert
        Assert.NotNull(exception);
    }
}
