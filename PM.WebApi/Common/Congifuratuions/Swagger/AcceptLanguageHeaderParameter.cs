using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace PM.WebApi.Common.Congifuratuions.Swagger;

/// <summary>
/// 
/// </summary>
public class AcceptLanguageHeaderParameter : IOperationFilter
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="operation"></param>
    /// <param name="context"></param>
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
