using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using PM.Application.Common.Constants;
using PM.Application.Common.Policies.ProjectOfUser;

namespace PM.Application.Common.Policies;

public static class PolicyConfig
{
    public static IServiceCollection AddPolicyConfig(this IServiceCollection services)
    {
        services.AddSingleton<IAuthorizationHandler, ProjectPolicyHandler>();

        services.AddAuthorization(options =>
        {
            options.AddPolicy(PolicyConstants.ProjectOfUser, policy =>
                policy.Requirements.Add(new ProjectPolicyRequirement()));
        });

        return services;
    }
}
