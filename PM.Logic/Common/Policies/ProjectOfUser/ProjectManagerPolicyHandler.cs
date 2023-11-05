using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using PM.Application.Common.Interfaces.IRepositories;
using PM.Domain.Common.Constants;
using System.Security.Claims;

namespace PM.Application.Common.Policies.ProjectOfUser;

public class ProjectManagerPolicyHandler : AuthorizationHandler<ProjectManagerPolicyRequirement>
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ProjectManagerPolicyHandler(
        IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        ProjectManagerPolicyRequirement requirement)
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

        if (httpContext is not null)
        {
            int projectId = Convert.ToInt32(httpContext.Request.RouteValues["id"]);

            using var serviceScope = httpContext.RequestServices.CreateScope();
            var dbContext = serviceScope.ServiceProvider.GetRequiredService<IProjectRepository>();

            var project = await dbContext
                .GetOrDeafaultAsync(p =>
                    p.Id == projectId &&
                    p.Manager != null &&
                    p.Manager.Id == userId, CancellationToken.None);

            if (project is not null)
                context.Succeed(requirement);
        }
    }
}
