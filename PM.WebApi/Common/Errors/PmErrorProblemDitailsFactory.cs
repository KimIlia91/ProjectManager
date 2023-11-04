using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Options;
using PM.WebApi.Common.Http;
using System.Diagnostics;

namespace PM.WebApi.Common.Errors;

/// <summary>
/// PmErrorProblemDitailsFactory class for creating ProblemDetails objects.
/// </summary>
public class PmErrorProblemDitailsFactory : ProblemDetailsFactory
{
    private readonly ApiBehaviorOptions _options;
    private readonly Action<ProblemDetailsContext>? _configure;

    /// <summary>
    /// Constructor for PmErrorProblemDitailsFactory.
    /// </summary>
    /// <param name="options">The API behavior options.</param>
    /// <param name="problemDetailsOptions">Optional problem details options.</param>
    /// <exception cref="ArgumentNullException">Thrown when options are null.</exception>
    public PmErrorProblemDitailsFactory(
        IOptions<ApiBehaviorOptions> options,
        IOptions<ProblemDetailsOptions>? problemDetailsOptions = null)
    {
        _options = options?.Value ?? throw new ArgumentNullException(nameof(options));
        _configure = problemDetailsOptions?.Value?.CustomizeProblemDetails;
    }

    /// <summary>
    /// Create a ProblemDetails object.
    /// </summary>
    /// <param name="httpContext">The HttpContext.</param>
    /// <param name="statusCode">The HTTP status code (default is 500).</param>
    /// <param name="title">The title (optional).</param>
    /// <param name="type">The type (optional).</param>
    /// <param name="detail">The detail (optional).</param>
    /// <param name="instance">The instance (optional).</param>
    /// <returns>The created ProblemDetails object.</returns>
    public override ProblemDetails CreateProblemDetails(
        HttpContext httpContext, 
        int? statusCode = null, 
        string? title = null, 
        string? type = null, 
        string? detail = null, 
        string? instance = null)
    {
        statusCode ??= 500;

        var problemDetails = new ProblemDetails
        {
            Status = statusCode,
            Title = title,
            Type = type,
            Detail = detail,
            Instance = instance,
        };

        ApplyProblemDetailsDefaults(httpContext, problemDetails, statusCode.Value);

        return problemDetails;
    }

    /// <summary>
    /// Create a ValidationProblemDetails object.
    /// </summary>
    /// <param name="httpContext">The HttpContext.</param>
    /// <param name="modelStateDictionary">The ModelStateDictionary.</param>
    /// <param name="statusCode">The HTTP status code (default is 400).</param>
    /// <param name="title">The title (optional).</param>
    /// <param name="type">The type (optional).</param>
    /// <param name="detail">The detail (optional).</param>
    /// <param name="instance">The instance (optional).</param>
    /// <returns>The created ValidationProblemDetails object.</returns>
    public override ValidationProblemDetails CreateValidationProblemDetails(
        HttpContext httpContext, 
        ModelStateDictionary modelStateDictionary, 
        int? statusCode = null, 
        string? title = null, 
        string? type = null, 
        string? detail = null, 
        string? instance = null)
    {
        ArgumentNullException.ThrowIfNull(modelStateDictionary);

        statusCode ??= 400;

        var problemDetails = new ValidationProblemDetails(modelStateDictionary)
        {
            Status = statusCode,
            Type = type,
            Detail = detail,
            Instance = instance,
        };

        if (title != null)
            problemDetails.Title = title;

        ApplyProblemDetailsDefaults(httpContext, problemDetails, statusCode.Value);

        return problemDetails;
    }

    /// <summary>
    /// Apply default settings to a ProblemDetails object.
    /// </summary>
    /// <param name="httpContext">The HttpContext.</param>
    /// <param name="problemDetails">The ProblemDetails object to modify.</param>
    /// <param name="statusCode">The HTTP status code.</param>
    private void ApplyProblemDetailsDefaults(
        HttpContext httpContext, 
        ProblemDetails problemDetails, 
        int statusCode)
    {
        problemDetails.Status ??= statusCode;

        if (_options.ClientErrorMapping.TryGetValue(statusCode, out var clientErrorData))
        {
            problemDetails.Title ??= clientErrorData.Title;
            problemDetails.Type ??= clientErrorData.Link;
        }

        var traceId = Activity.Current?.Id ?? httpContext?.TraceIdentifier;
        if (traceId != null)
            problemDetails.Extensions["traceId"] = traceId;

        var errors = httpContext?.Items[HttpContextItemKeys.Errors] as List<Error>;

        if (errors is not null)
            problemDetails.Extensions.Add("errorCodes", errors.Select(e => e.Code));
    }
}
