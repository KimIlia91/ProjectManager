using Microsoft.AspNetCore.Authorization;

namespace PM.Application.Common.Policies.ProjectOfUser;

public class ProjectPolicyRequirement : IAuthorizationRequirement
{
    public ProjectPolicyRequirement()
    {
    }
}
