using System.Reflection;
using MediatR;
using ErrorOr;
using FluentValidation;
using FluentValidation.Results;

namespace PM.Application.Common.Behaviors;

/// <summary>
/// A behavior for request validation in MediatR pipeline.
/// </summary>
/// <typeparam name="TRequest">The type of the request.</typeparam>
/// <typeparam name="TResponse">The type of the response.</typeparam>
public class ValidationBehavior<TRequest, TResponse> 
    : IPipelineBehavior<TRequest, TResponse> 
    where TRequest : IRequest<TResponse> 
    where TResponse : IErrorOr
{
    private readonly IValidator<TRequest>? _validator;

    /// <summary>
    /// Initializes a new instance of the <see cref="ValidationBehavior{TRequest, TResponse}"/> class.
    /// </summary>
    /// <param name="validator">The validator for the request.</param>
    public ValidationBehavior(IValidator<TRequest>? validator = null) => _validator = validator;

    /// <summary>
    /// Handles request validation in the MediatR pipeline.
    /// </summary>
    /// <param name="request">The request to validate.</param>
    /// <param name="next">The delegate for the next handler in the pipeline.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The response after validation.</returns>
    /// <exception cref="ValidationException">Thrown when validation fails.</exception>
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (_validator == null) return await next();

        var validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if (validationResult.IsValid) return await next();

        return TryCreateResponseFromErrors(validationResult.Errors, out var response)
            ? response
            : throw new ValidationException(validationResult.Errors);
    }

    /// <summary>
    /// Tries to create a response from validation errors.
    /// </summary>
    /// <param name="validationFailures">The list of validation failures.</param>
    /// <param name="response">The resulting response if successfully created.</param>
    /// <returns>A boolean value indicating if the response was successfully created.</returns>
    private static bool TryCreateResponseFromErrors(
        List<ValidationFailure> validationFailures,
        out TResponse response)
    {
        List<Error> errors = validationFailures.ConvertAll(x => Error.Validation(
                code: x.PropertyName,
                description: x.ErrorMessage));

        response = (TResponse?)typeof(TResponse)
            .GetMethod(
                name: nameof(ErrorOr<object>.From),
                bindingAttr: BindingFlags.Static | BindingFlags.Public,
                types: new[] { typeof(List<Error>) })?
            .Invoke(null, new[] { errors })!;

        return response is not null;
    }
}
