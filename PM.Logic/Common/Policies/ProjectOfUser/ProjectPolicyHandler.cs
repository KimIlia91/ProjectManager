using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using PM.Application.Common.Interfaces.IRepositories;
using PM.Application.Features.TaskContext.Commands.CreateTask.UserSpec;
using PM.Domain.Common.Constants;
using System.Security.Claims;

namespace PM.Application.Common.Policies.ProjectOfUser;

public class ProjectPolicyHandler : AuthorizationHandler<ProjectPolicyRequirement>
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ProjectPolicyHandler(
        IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        ProjectPolicyRequirement requirement)
    {
        if (context.User.IsInRole(RoleConstants.Supervisor))
        {
            context.Succeed(requirement);
            return;
        }

        var userClaim = context.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier);

        if (userClaim is null)
            return;

        if (!int.TryParse(userClaim.Value, out int userId))
            return;

        var httpContext = _httpContextAccessor.HttpContext;
        int projectId = Convert.ToInt32(httpContext.Request.RouteValues["id"]);

        var getUserOfProject = new GetUserOfRpojectSpec(userId, projectId);

        using var serviceScope = httpContext.RequestServices.CreateScope();
        var dbContext = serviceScope.ServiceProvider.GetRequiredService<IUserRepository>();
        var user = await dbContext
            .GetOrDeafaultAsync(getUserOfProject.ToExpression(), CancellationToken.None);

        if (user is not null)
            context.Succeed(requirement);
    }
}
