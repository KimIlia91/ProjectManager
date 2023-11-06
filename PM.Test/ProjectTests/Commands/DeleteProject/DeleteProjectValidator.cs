using PM.Application.Common.Resources;
using PM.Application.Features.ProjectContext.Commands.DeleteProject;
using PM.Test.Common.FakeRepositories;

namespace PM.Test.ProjectTests.Commands.DeleteProject;

public sealed class DeleteProjectValidator
{
    private readonly FakeProjectRepository _projectRepository;

    public DeleteProjectValidator()
    {
        _projectRepository = new FakeProjectRepository();
    }

    [Fact]
    public async Task Validator_Should_ReturnDeleteProjectResult_WhenAllIsValid()
    {
        //Arrange
        var command = new DeleteProjectCommand() { Id = 1 };
        var validator = new DeleteProjectCommandValidator(_projectRepository);

        //Act
        var result = await validator.ValidateAsync(command);

        //Assert
        Assert.True(result.IsValid);
        Assert.Empty(result.Errors);
    }

    [Fact]
    public async Task Validator_Should_ReturnProjectIdErrorMessage_WhenProjectIdNotFound()
    {
        //Arrange
        var command = new DeleteProjectCommand() { Id = 1001 };
        var validator = new DeleteProjectCommandValidator(_projectRepository);

        //Act
        var result = await validator.ValidateAsync(command);

        //Assert
        Assert.False(result.IsValid);
        Assert.Single(result.Errors);
        Assert.Equal(ErrorsResource.NotFound, result.Errors.First().ErrorMessage);
    }

    [Fact]
    public async Task Validator_Should_ReturnProjectIdErrorMessage_WhenProjectIdIsEmpty()
    {
        //Arrange
        var command = new DeleteProjectCommand();
        var validator = new DeleteProjectCommandValidator(_projectRepository);

        //Act
        var result = await validator.ValidateAsync(command);

        //Assert
        Assert.False(result.IsValid);
        Assert.Single(result.Errors);
        Assert.Equal(ErrorsResource.Required, result.Errors.First().ErrorMessage);
    }
}
