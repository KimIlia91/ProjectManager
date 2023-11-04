using Microsoft.EntityFrameworkCore;
using Moq;
using PM.Application.Common.Interfaces.ISercices;
using PM.Application.Features.UserContext.Commands.CreateUser;
using PM.Domain.Entities;
using PM.Infrastructure.Persistence;
using Task = System.Threading.Tasks.Task;

namespace PM.Tests
{
    public class CreateUserCommandHandlerTests
    {
        [Fact]
        public async Task Handle_SuccessfulRegistratio_ReturnsCreateUserResult()
        {
           //var options = new DbContextOptionsBuilder<ApplicationDbContext>()
           //    .UseInMemoryDatabase()
        }


        [Fact]
        public async Task Handle_SuccessfulRegistration_ReturnsCreateUserResult()
        {
            // Arrange
            var identityServiceMock = new Mock<IIdentityService>();
            identityServiceMock
                .Setup(x => x.RegisterAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<User>()))
                    .ReturnsAsync(User.Create("John", "Doe", "Middle"));


            var handler = new CreateUserCommandHandler(identityServiceMock.Object);
            var command = new CreateUserCommand(
                "John",
                "Doe",
                "Middle",
                "john.doe@example.com",
                "Password123!",
                "Сотрудник");

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.False(result.IsError);
            Assert.Equal(0, result.Value.UserId);
        }

        [Fact]
        public async Task Handle_RegistrationFails_ReturnsError()
        {
            var tsetUserResult = User.Create(
                "John",
                "Doe",
                "Middle");

            // Arrange
            var identityServiceMock = new Mock<IIdentityService>();
            identityServiceMock
                .Setup(x => x.RegisterAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<User>()))
                    .ReturnsAsync(tsetUserResult);

            var handler = new CreateUserCommandHandler(identityServiceMock.Object);
            var command = new CreateUserCommand(
                null,
                "Doe",
                "john.doe@example.com",
                "Middle",
                "Password123!",
                "Сотрудник");

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.IsError);
        }
    }
}