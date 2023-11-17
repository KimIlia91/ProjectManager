using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using PM.Application.Features.UserContext.Commands.UpdateUser;
using PM.Domain.Common.Errors;
using PM.Test.Common.Constants;
using PM.Test.Common.FakeServices;

namespace PM.Test.UserTests.Commands.UpdateUser;

public class UpdateUserHandler
{
    private readonly FakeIdentityService _service;
    private readonly string _testUserEmail;
    private readonly IMapper _mapper;

    public UpdateUserHandler()
    {
        _service = new FakeIdentityService();
        _testUserEmail = TestDataConstants.TestUserEmail;
        _mapper = new Mapper();
    }

    [Fact]
    public async Task Handler_Should_ReturnUpdateUserResult_WhenAllValid()
    {
        //Arrange
        var firstName = Guid.NewGuid().ToString();
        var lastName = Guid.NewGuid().ToString();
        var email = "newEmail@user.com";
        var middleName = Guid.NewGuid().ToString();

        var user = await _service.Get().Users
            .SingleOrDefaultAsync(u => u.Email == _testUserEmail);

        var command = new UpdateUserCommand()
        {
            User = user,
            Id = user.Id,
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            MiddleName = middleName,
        };

        var handler = new UpdateUserCommandHandler(_mapper, _service);

        //Act
        var result = await handler.Handle(command, CancellationToken.None);

        //Assert
        Assert.NotNull(result.Value);
        Assert.False(result.IsError);
        Assert.Equal(user.LastName, result.Value.LastName);
        Assert.Equal(user.FirstName, result.Value.FirstName);
        Assert.Equal(user.MiddleName, result.Value.MiddleName);
        Assert.Equal(user.Email, result.Value.Email);
    }

    [Fact]
    public async Task Handler_Should_ReturnMiddleNameValid_WhenMiddleNameIsNull()
    {
        //Arrange
        var firstName = Guid.NewGuid().ToString();
        var lastName = Guid.NewGuid().ToString();
        var email = "newEmail@user.com";

        var user = await _service.Get().Users
            .SingleOrDefaultAsync(u => u.Email == _testUserEmail);

        var command = new UpdateUserCommand()
        {
            User = user,
            Id = user.Id,
            FirstName = firstName,
            LastName = lastName,
            Email = email
        };

        var handler = new UpdateUserCommandHandler(_mapper, _service);

        //Act
        var result = await handler.Handle(command, CancellationToken.None);

        //Assert
        Assert.False(result.IsError);
        Assert.Equal(user.LastName, result.Value.LastName);
        Assert.Equal(user.FirstName, result.Value.FirstName);
        Assert.Null(result.Value.MiddleName);
        Assert.Equal(user.Email, result.Value.Email);
    }

    [Fact]
    public async Task Handler_Should_ReturnFirstNameError_WhenFirstNameIsEmpty()
    {
        //Arrange
        var firstName = string.Empty;
        var lastName = Guid.NewGuid().ToString();
        var email = "newEmail@user.com";
        var middleName = Guid.NewGuid().ToString();

        var user = await _service.Get().Users
            .SingleOrDefaultAsync(u => u.Email == _testUserEmail);

        var command = new UpdateUserCommand()
        {
            User = user,
            Id = user.Id,
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            MiddleName = middleName,
        };

        var handler = new UpdateUserCommandHandler(_mapper, _service);

        //Act
        var result = await handler.Handle(command, CancellationToken.None);

        //Assert
        Assert.True(result.IsError);
        Assert.Single(result.Errors);
        Assert.Equal(Errors.User.FirstNameRequired.Description, result.Errors.First().Description);
    }

    [Fact]
    public async Task Handler_Should_ReturnLastNameError_WhenLastNameIsEmpty()
    {
        //Arrange
        var firstName = Guid.NewGuid().ToString();
        var lastName = string.Empty;
        var email = "newEmail@user.com";
        var middleName = Guid.NewGuid().ToString();

        var user = await _service.Get().Users
            .SingleOrDefaultAsync(u => u.Email == _testUserEmail);

        var command = new UpdateUserCommand()
        {
            User = user,
            Id = user.Id,
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            MiddleName = middleName,
        };

        var handler = new UpdateUserCommandHandler(_mapper, _service);

        //Act
        var result = await handler.Handle(command, CancellationToken.None);

        //Assert
        Assert.True(result.IsError);
        Assert.Single(result.Errors);
        Assert.Equal(Errors.User.LastNameRequired.Description, result.Errors.First().Description);
    }

    [Fact]
    public async Task Handler_Should_ReturnEmailError_WhenEmailInvalid()
    {
        //Arrange
        var firstName = Guid.NewGuid().ToString();
        var lastName = Guid.NewGuid().ToString();
        var email = "newEmail@use";

        var user = await _service.Get().Users
            .SingleOrDefaultAsync(u => u.Email == _testUserEmail);

        var command = new UpdateUserCommand()
        {
            User = user,
            Id = user.Id,
            FirstName = firstName,
            LastName = lastName,
            Email = email
        };

        var handler = new UpdateUserCommandHandler(_mapper, _service);

        //Act
        var result = await handler.Handle(command, CancellationToken.None);

        //Assert
        Assert.True(result.IsError);
        Assert.Single(result.Errors);
        Assert.Equal(Errors.User.InvalidEmail.Description, result.Errors.First().Description);
    }
}
