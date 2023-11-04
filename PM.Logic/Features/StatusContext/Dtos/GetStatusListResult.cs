using Mapster;
using PM.Domain.Common.Enums;
using PM.Domain.Common.Extensions;

namespace PM.Application.Features.StatusContext.Dtos;

/// <summary>
/// Represents the result for retrieving a list of statuses and registering mappings.
/// </summary>
public class GetStatusListResult : IRegister
{
    /// <summary>
    /// Gets or sets the name of the status.
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Registers mappings between the 'Status' enum and 'GetStatusListResult'.
    /// </summary>
    /// <param name="config">The type adapter configuration.</param>
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Status, GetStatusListResult>()
            .Map(dest => dest.Name, src => src.GetDescription());
    }
}
