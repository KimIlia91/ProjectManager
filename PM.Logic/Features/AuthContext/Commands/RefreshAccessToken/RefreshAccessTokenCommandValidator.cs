using FluentValidation;
using PM.Application.Common.Resources;

namespace PM.Application.Features.AuthContext.Commands.RefreshAccessToken;

public sealed class RefreshAccessTokenCommandValidator
    : AbstractValidator<RefreshAccessTokenCommand>
{
    public RefreshAccessTokenCommandValidator()
    {
        RuleFor(command => command.RefreshToken)
            .NotEmpty()
            .WithMessage(ErrorsResource.Required);
    }
}
