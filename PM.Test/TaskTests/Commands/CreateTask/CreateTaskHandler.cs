using Moq;
using PM.Application.Common.Interfaces.IRepositories;
using PM.Application.Common.Interfaces.ISercices;
using PM.Application.Features.TaskContext.Commands.CreateTask;
using PM.Domain.Common.Enums;
using PM.Domain.Entities;
using PM.Test.Common.Constants;
using System.Linq.Expressions;
using Task = System.Threading.Tasks.Task;

namespace PM.Test.TaskTests.Commands.CreateTask;

public sealed class CreateTaskHandler
{
    private readonly Mock<ITaskRepository> _taskRepository;
    private readonly Mock<IUserRepository> _userRepository;
    private readonly Mock<ICurrentUserService> _currentUserService;

    public CreateTaskHandler()
    {
        _currentUserService = new Mock<ICurrentUserService>();
        _taskRepository = new Mock<ITaskRepository>();
        _userRepository = new Mock<IUserRepository>();

        _userRepository.Setup(repo => repo.GetOrDeafaultAsync(
                It.IsAny<Expression<Func<User, bool>>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(User.Create(
                TestDataConstants.TestUserFirstName,
                TestDataConstants.TestUserLastName,
                TestDataConstants.TestUserEmail).Value);

        _currentUserService.Setup(service => service.UserId).Returns(1);
    }

    [Fact]
    public async Task Handler_Should_ReturnCreateTaskSuccess_WhenAllIsValid()
    {
        // Arrange
        var executor = User.Create(
                TestDataConstants.TestUserFirstName,
                TestDataConstants.TestUserLastName,
                TestDataConstants.TestUserEmail).Value;

        var project = Project.Create(
            TestDataConstants.TestProjectName,
            TestDataConstants.TestCustomerCompany,
            TestDataConstants.TestExecutorCompany,
            executor,
            TestDataConstants.StartDate,
            TestDataConstants.EndDate,
            Priority.High).Value;

        var command = new CreateTaskCommand()
        {
            Name = "Title",
            Executor = executor,
            ExecutorId = 2,
            Project = project,
            ProjectId = 1,
            Status = Status.ToDo,
            Priority = Priority.Medium
        };

        var handler = new CreateTaskCommandHandler(
            _taskRepository.Object, _userRepository.Object, _currentUserService.Object);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.IsError);
    }
}
