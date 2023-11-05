using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using PM.Application.Common.Interfaces.IRepositories;
using PM.Domain.Common.Constants;
using System.Security.Claims;

namespace PM.Application.Common.Policies.TaskOfUser;

public sealed class TaskOfUserPolicyHandler : AuthorizationHandler<TaskOfUserPolicyRequirement>
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public TaskOfUserPolicyHandler(
        IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        TaskOfUserPolicyRequirement requirement)
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
            int taskId = Convert.ToInt32(httpContext.Request.RouteValues["id"]);

            using var serviceScope = httpContext.RequestServices.CreateScope();
            var dbContext = serviceScope.ServiceProvider.GetRequiredService<ITaskRepository>();

            var task = await dbContext
                .GetOrDeafaultAsync(t => t.Id == taskId &&
                    t.Project.Users.Any(u => u.Id == userId), CancellationToken.None);

            if (task is not null)
                context.Succeed(requirement);
        }
    }
}
