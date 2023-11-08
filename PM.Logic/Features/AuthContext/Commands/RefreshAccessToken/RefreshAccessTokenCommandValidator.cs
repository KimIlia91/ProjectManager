using FluentValidation;
using PM.Application.Common.Resources;

namespace PM.Application.Features.AuthContext.Commands.RefreshAccessToken;

/// <summary>
/// Validator for the RefreshAccessTokenCommand, 
/// which ensures that the refresh token is provided.
/// </summary>
public sealed class RefreshAccessTokenCommandValidator
    : AbstractValidator<RefreshAccessTokenCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RefreshAccessTokenCommandValidator"/> class.
    /// </summary>
    public RefreshAccessTokenCommandValidator()
    {
        RuleFor(command => command.RefreshToken)
            .NotEmpty()
            .WithMessage(ErrorsResource.Required);
    }
}
