using Microsoft.EntityFrameworkCore;
using PM.Application.Features.ProjectContext.Commands.UpdateProject;
using PM.Domain.Common.Errors;
using PM.Test.Common.FakeRepositories;

namespace PM.Test.ProjectTests.Commands.UpdateProject;

public sealed class UpdateProjectHandler
{
    private readonly FakeProjectRepository _projectRepository;

    public UpdateProjectHandler()
    {
        var guid = Guid.NewGuid();
        _projectRepository = new FakeProjectRepository(guid);
    }

    [Fact]
    public async Task Handler_Should_ReturnUpdateProjectResult_WhenAllAreValid()
    {
        var project = await _projectRepository.Context.Projects
            .FirstAsync(p => p.Id == 1);

        //Arrange
        var command = new UpdateProjectCommand()
        {
            Project = project,
            Id = project.Id,
            Name = "Test",
            CustomerCompany = "Test",
            ExecutorCompany = "Test",
            ManagerId = 2,
            StartDate = DateTime.UtcNow.AddDays(2),
            EndDate = DateTime.UtcNow.AddDays(4),
            Priority = Domain.Common.Enums.Priority.High
        };

        var handler = new UpdateProjectCommandHandler(
            _projectRepository, _projectRepository.Mapper);

        //Act
        var result = await handler.Handle(command, CancellationToken.None);
        var expectedProject = await _projectRepository
            .GetOrDeafaultAsync(p => p.Id == command.Id &&
                p.Name == command.Name &&
                p.CustomerCompany == command.CustomerCompany &&
                p.ExecutorCompany == command.ExecutorCompany &&
                p.Manager.Id == command.ManagerId &&
                p.StartDate == command.StartDate &&
                p.EndDate == command.EndDate &&
                p.Priority == command.Priority, CancellationToken.None);

        //Assert
        Assert.False(result.IsError);
        Assert.NotNull(result.Value);
    }


    [Fact]
    public async Task Handler_Should_ReturnInvalidDate_WhenStartDateMoreThanEnaDate()
    {
        var project = await _projectRepository.Context.Projects
            .FirstAsync(p => p.Id == 1);

        //Arrange
        var command = new UpdateProjectCommand()
        {
            Project = project,
            Id = project.Id,
            Name = "Test",
            CustomerCompany = "Test",
            ExecutorCompany = "Test",
            ManagerId = 2,
            StartDate = DateTime.UtcNow.AddDays(4),
            EndDate = DateTime.UtcNow.AddDays(2),
            Priority = Domain.Common.Enums.Priority.High
        };

        var handler = new UpdateProjectCommandHandler(
            _projectRepository, _projectRepository.Mapper);

        //Act
        var result = await handler.Handle(command, CancellationToken.None);

        //Assert
        Assert.True(result.IsError);
        Assert.Equal(Errors.Project.InvalidDate.Description, result.Errors.First().Description);
    }
}
