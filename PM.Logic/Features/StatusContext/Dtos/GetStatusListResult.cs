using Mapster;
using PM.Domain.Common.Enums;
using PM.Domain.Common.Extensions;

namespace PM.Application.Features.StatusContext.Dtos;

public class GetStatusListResult : IRegister
{
    public string Name { get; set; } = null!;

    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Status, GetStatusListResult>()
            .Map(dest => dest.Name, src => src.GetDescription());
    }
}
