using Microsoft.EntityFrameworkCore;
using PM.Application.Features.EmployeeProjectsContext.Commands.RemoveEmployeeFromProject;
using PM.Test.Common.FakeRepositories;
using PM.Test.Common.FakeServices;
using System;

namespace PM.Test.UserProjectTests.Commands.RemoveUserFromProject;

public sealed class RemoveUserFromProjectHandler
{
    private readonly FakeProjectRepository _projectRepository;

    public RemoveUserFromProjectHandler()
    {
        _projectRepository = new FakeProjectRepository();
    }

    [Fact]
    public async Task Handler_Should_ReturnSuccessResult_WhenAllAreValid()
    {
        //Arrange
        var project = await _projectRepository.Context.Projects
            .SingleAsync(p => p.Id == 2);

        var user = await _projectRepository.Context.Users
            .FirstAsync(u => u.Id == 2);

        var query = new RemoveUserFromProjectCommand()
        {
            Project = project,
            ProjectId = project.Id,
            User = user,
            UserId = user.Id
        };

        var handler = new RemoveUserFromProjectCommandHandler(_projectRepository);

        //Act
        var result = await handler.Handle(query, CancellationToken.None);

        //Assert
        Assert.False(result.IsError);
        Assert.Single(result.Errors);
        Assert.Equal(query.UserId, result.Value.UserId);
    }
}
