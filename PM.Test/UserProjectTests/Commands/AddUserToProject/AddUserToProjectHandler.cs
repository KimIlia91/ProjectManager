using Microsoft.EntityFrameworkCore;
using PM.Application.Features.EmployeeProjectsContext.Commands.AddEmployeeToProject;
using PM.Test.Common.FakeRepositories;

namespace PM.Test.UserProjectTests.Commands.AddUserToProject;

public sealed class AddUserToProjectHandler
{
    private readonly FakeProjectRepository _projectRepository;

    public AddUserToProjectHandler()
    {
        _projectRepository = new FakeProjectRepository();
    }

    [Fact]
    public async Task Handler_Should_ReturnAddUserToProjectResult_WhenUserIsNotInProjectYet()
    {
        //Arrange
        var project = await _projectRepository
            .GetOrDeafaultAsync(p => p.Id == 2, CancellationToken.None);

        var user = await _projectRepository.Context.Users
            .FirstAsync(u => u.Id == 3);

        var command = new AddUserToProjectCommand()
        {
            Project = project,
            Employee = user,
            ProjectId = project.Id,
            UserId = user.Id
        };

        var handler = new AddUserToProjectCommandHandler(_projectRepository);

        //Act
        var result = await handler.Handle(command, CancellationToken.None);
        var projectFromDb = await _projectRepository.Context.Projects
            .Include(p => p.Users)
            .FirstAsync(p => p.Id == 2);

        var expectedUser = projectFromDb.Users.FirstOrDefault(u => u.Id == 3);

        //Assert
        Assert.False(result.IsError);
        Assert.Equal(command.UserId, result.Value.EmployeeId);
        Assert.NotNull(expectedUser);
        Assert.Equal(expectedUser.Id, command.UserId);
    }

    [Fact]
    public async Task Handler_Should_ReturnOperationException_WhenUserIsAlreadyInProject()
    {
        //Arrange
        var project = await _projectRepository
            .GetOrDeafaultAsync(p => p.Id == 2, CancellationToken.None);

        var user = await _projectRepository.Context.Users
            .FirstAsync(u => u.Id == 2);

        var command = new AddUserToProjectCommand()
        {
            Project = project,
            Employee = user,
            ProjectId = project.Id,
            UserId = user.Id
        };

        var handler = new AddUserToProjectCommandHandler(_projectRepository);

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(async () =>
        {
            await handler.Handle(command, CancellationToken.None);
        });
    }
}
