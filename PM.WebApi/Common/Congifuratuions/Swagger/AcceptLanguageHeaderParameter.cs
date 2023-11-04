using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace PM.WebApi.Common.Congifuratuions.Swagger;

/// <summary>
/// Operation filter for adding the 'Accept-Language' header parameter to OpenAPI operations.
/// </summary>
public class AcceptLanguageHeaderParameter : IOperationFilter
{
    /// <summary>
    /// Applies the 'Accept-Language' header parameter to the specified OpenAPI operation.
    /// </summary>
    /// <param name="operation">The OpenApiOperation to modify.</param>
    /// <param name="context">The OperationFilterContext providing information about the operation.</param>
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        operation.Parameters ??= new List<OpenApiParameter>();

        operation.Parameters.Add(new OpenApiParameter()
        {
            Name = "Accept-Language",
            In = ParameterLocation.Header
        });
    }
}
