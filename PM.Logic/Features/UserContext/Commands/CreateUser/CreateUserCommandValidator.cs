using FluentValidation;
using PM.Application.Common.Interfaces.IRepositories;
using PM.Application.Common.Interfaces.ISercices;
using PM.Application.Common.Resources;
using PM.Domain.Common.Constants;

namespace PM.Application.Features.UserContext.Commands.CreateUser;

public sealed class CreateUserCommandValidator
    : AbstractValidator<CreateUserCommand>
{
    private readonly IRoleRepository _roleRepository;
    private readonly IUserRepository _employeeRepository;
    private readonly IIdentityService _identityService;

    public CreateUserCommandValidator(
        IRoleRepository roleRepository,
        IUserRepository employeeRepository,
        IIdentityService identityService)
    {
        _identityService = identityService;
        _roleRepository = roleRepository;
        _employeeRepository = employeeRepository;

        RuleFor(command => command.FirstName)
            .NotEmpty()
            .WithMessage(ErrorsResource.Required)
            .MaximumLength(EntityConstants.FirstName)
            .WithMessage(string.Format(ErrorsResource.MaxLength, EntityConstants.FirstName));

        RuleFor(command => command.LastName)
            .NotEmpty()
            .WithMessage(ErrorsResource.Required)
            .MaximumLength(EntityConstants.LastName)
            .WithMessage(string.Format(ErrorsResource.MaxLength, EntityConstants.LastName));

        RuleFor(command => command.MiddelName)
            .MaximumLength(EntityConstants.MiddelName)
            .When(command => command.MiddelName is not null)
            .WithMessage(string.Format(ErrorsResource.MaxLength, EntityConstants.MiddelName));

        RuleFor(command => command.Email)
            .NotEmpty()
            .WithMessage(ErrorsResource.Required)
            .EmailAddress()
            .WithMessage(ErrorsResource.InvalidEmail)
            .MaximumLength(EntityConstants.Email)
            .WithMessage(string.Format(ErrorsResource.MaxLength, EntityConstants.Email))
            .MustAsync(MustBeUnique)
            .WithMessage(ErrorsResource.NotFound);

        RuleFor(command => command.RoleName)
            .NotEmpty()
            .WithMessage(ErrorsResource.Required)
            .MaximumLength(EntityConstants.RoleNameLength)
            .WithMessage(string.Format(ErrorsResource.MaxLength, EntityConstants.RoleNameLength))
            .MustAsync(MustBeInDatabase)
            .WithMessage(ErrorsResource.NotFound);
    }

    private async Task<bool> MustBeInDatabase(
        string roleName,
        CancellationToken cancellationToken)
    {
        return await _identityService.IsRoleExistAsync(roleName);
    }

    private async Task<bool> MustBeUnique(
        string email,
        CancellationToken cancellationToken)
    {
        var emailExist = await _employeeRepository
          .GetOrDeafaultAsync(e => e.Email == email, cancellationToken);

        return emailExist is null;
    }
}
