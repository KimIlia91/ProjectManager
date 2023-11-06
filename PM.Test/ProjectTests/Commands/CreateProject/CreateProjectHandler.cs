using Microsoft.EntityFrameworkCore;
using PM.Application.Features.ProjectContext.Commands.CreateProject;
using PM.Domain.Common.Enums;
using PM.Domain.Common.Errors;
using PM.Test.Common.FakeRepositories;

namespace PM.Test.ProjectTests.Commands.CreateProject;

public sealed class CreateProjectHandler
{
    private readonly FakeProjectRepository _projectRepository;

    public CreateProjectHandler()
    {
        var guid = Guid.NewGuid();
        _projectRepository = new FakeProjectRepository(guid);
    }

    [Fact]
    public async Task Handler_Should_ReturnCreateProjectResult_WhenAllValid()
    {
        var manager = await _projectRepository.Context.Users
            .FirstOrDefaultAsync(u => u.Id == 2);

        //Arrange
        var command = new CreateProjectCommand()
        {
            Manager = manager,
            Name = "Name",
            CustomerCompany = "Customer Company",
            ExecutorCompany = "Executor Company",
            ManagerId = manager.Id,
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddMonths(1),
            Priority = Priority.Low
        };

        var handler = new CreateProjectCommandHandler(_projectRepository);

        //Act
        var result = await handler.Handle(command, CancellationToken.None);
        var expectedProject = await _projectRepository
            .GetOrDeafaultAsync(p => p.Name == command.Name &&
                p.CustomerCompany == command.CustomerCompany &&
                p.ExecutorCompany == command.ExecutorCompany &&
                p.Manager.Id == command.ManagerId &&
                p.StartDate == command.StartDate &&
                p.EndDate == command.EndDate &&
                p.Priority == command.Priority, CancellationToken.None);

        //Assert
        Assert.False(result.IsError);
        Assert.NotNull(expectedProject);
    }


    [Fact]
    public async Task Handler_Should_ReturnValidationDateError_WhenStartDateMoreThanEndDate()
    {
        var manager = await _projectRepository.Context.Users
            .FirstOrDefaultAsync(u => u.Id == 2);

        //Arrange
        var command = new CreateProjectCommand()
        {
            Manager = manager,
            Name = "Name",
            CustomerCompany = "Customer Company",
            ExecutorCompany = "Executor Company",
            ManagerId = manager.Id,
            StartDate = DateTime.UtcNow.AddMonths(1),
            EndDate = DateTime.UtcNow,
            Priority = Priority.Low
        };

        var handler = new CreateProjectCommandHandler(_projectRepository);

        //Act
        var result = await handler.Handle(command, CancellationToken.None);

        //Assert
        Assert.True(result.IsError);
        Assert.Equal(Errors.Project.InvalidDate.Description, result.Errors.First().Description);
    }
}
