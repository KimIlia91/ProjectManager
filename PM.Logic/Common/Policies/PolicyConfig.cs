using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using PM.Application.Common.Constants;
using PM.Application.Common.Policies.ProjectOfUser;
using PM.Application.Common.Policies.TaskManager;
using PM.Application.Common.Policies.TaskOfUser;

namespace PM.Application.Common.Policies;

public static class PolicyConfig
{
    public static IServiceCollection AddPolicyConfig(this IServiceCollection services)
    {
        services.AddSingleton<IAuthorizationHandler, ProjectManagerPolicyHandler>();
        services.AddSingleton<IAuthorizationHandler, TaskOfUserPolicyHandler>();

        services.AddAuthorization(options =>
        {
            options.AddPolicy(PolicyConstants.ProjectManagerPolicy, policy =>
                policy.Requirements.Add(new ProjectManagerPolicyRequirement()));

            options.AddPolicy(PolicyConstants.TaskOfUserPolicy, policy =>
                policy.Requirements.Add(new TaskOfUserPolicyRequirement()));

            options.AddPolicy(PolicyConstants.TaskManagerPolicy, policy =>
                policy.Requirements.Add(new TaskManagerPolicyRequirement()));
        });

        return services;
    }
}
