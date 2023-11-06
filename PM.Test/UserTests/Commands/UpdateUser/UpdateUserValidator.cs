using MapsterMapper;
using PM.Application.Features.UserContext.Commands.CreateUser;
using PM.Application.Features.UserContext.Commands.UpdateUser;
using PM.Test.Common.Constants;
using PM.Test.Common.FakeRepositories;

namespace PM.Test.UserTests.Commands.UpdateUser;

public class UpdateUserValidator
{
    private readonly FakeUserRepository _userRepository;
    private readonly string _testUserEmail;
    private readonly IMapper _mapper;

    public UpdateUserValidator()
    {
        _userRepository = new FakeUserRepository();
        _testUserEmail = TestDataConstants.TestUserEmail;
        _mapper = new Mapper(); 
    }

    [Fact]
    public async Task Validator_Should_ReturnValidResult_WhenAllValid()
    {
        //Arrange
        var firstName = Guid.NewGuid().ToString();
        var lastName = Guid.NewGuid().ToString();
        var email = "newEmail@user.com";
        var middleName = Guid.NewGuid().ToString();

        var user = await _userRepository
            .GetOrDeafaultAsync(u => u.Email == _testUserEmail, CancellationToken.None);

        var command = new UpdateUserCommand()
        {
            Id = user.Id,
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            MiddelName = middleName,
        };

        var validator = new UpdateUserCommandValidator(_userRepository);

        //Act
        var result = await validator.ValidateAsync(command);

        //Assert
        Assert.True(result.IsValid);
    }

    [Fact]
    public async Task Validator_Should_ReturnFirstName_WhenFirstNameIsEmpty()
    {
        //Arrange
        var firstName = string.Empty;
        var lastName = Guid.NewGuid().ToString();
        var email = "newEmail@user.com";
        var middleName = Guid.NewGuid().ToString();

        var user = await _userRepository
            .GetOrDeafaultAsync(u => u.Email == _testUserEmail, CancellationToken.None);

        var command = new UpdateUserCommand()
        {
            Id = user.Id,
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            MiddelName = middleName,
        };

        var validator = new UpdateUserCommandValidator(_userRepository);

        //Act
        var result = await validator.ValidateAsync(command);

        //Assert
        Assert.False(result.IsValid);
        Assert.Equal(nameof(command.FirstName), result.Errors.First().PropertyName);
    }

    [Fact]
    public async Task Validator_Should_ReturnFourErrors_WhenAllIsEmpty()
    {
        //Arrange
        var firstName = string.Empty;
        var lastName = string.Empty;
        var email = string.Empty;
        var middleName = string.Empty;

        var user = await _userRepository
            .GetOrDeafaultAsync(u => u.Email == _testUserEmail, CancellationToken.None);

        var command = new UpdateUserCommand()
        {
            Id = user.Id,
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            MiddelName = middleName,
        };

        var validator = new UpdateUserCommandValidator(_userRepository);

        //Act
        var result = await validator.ValidateAsync(command);

        var expectedFirstName = nameof(command.FirstName);
        var actualFirstName = result.Errors
            .First(e => e.PropertyName == expectedFirstName).PropertyName;

        var expectedLastName = nameof(command.LastName);
        var actualLastName = result.Errors
            .First(e => e.PropertyName == expectedLastName).PropertyName;

        var expectedEmail = nameof(command.Email);
        var actualEmail = result.Errors
            .First(e => e.PropertyName == expectedEmail).PropertyName;

        //Assert
        Assert.False(result.IsValid);
        Assert.Equal(4, result.Errors.Count);
        Assert.Equal(expectedFirstName, actualFirstName);
        Assert.Equal(expectedLastName, actualLastName);
        Assert.Equal(expectedEmail, actualEmail);
    }
}
