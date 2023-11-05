using Microsoft.AspNetCore.Authorization;

namespace PM.Application.Common.Policies.ProjectOfUser;

public class ProjectManagerPolicyRequirement : IAuthorizationRequirement
{
    public ProjectManagerPolicyRequirement()
    {
    }
}
