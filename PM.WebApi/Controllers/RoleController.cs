using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PM.Application.Features.RoleContext.Commands.GetRoleList;
using PM.Application.Features.RoleContext.Dtos;
using PM.Domain.Common.Constants;

namespace PM.WebApi.Controllers;

/// <summary>
/// Controller for managing user roles.
/// </summary>
public class RoleController : ApiBaseController
{
    /// <summary>
    /// Retrieve a list of user roles.
    /// </summary>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>
    /// An IActionResult representing the list of user roles, which can be one of the following:
    /// - 200 OK with the list of roles if successful.
    /// - A problem response with errors if there are issues.
    /// </returns>
    [HttpGet]
    [Authorize(Roles = RoleConstants.Supervisor)]
    [ProducesResponseType(typeof(List<GetRoleListResult>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetRoleListAsync(
        CancellationToken cancellationToken)
    {
        var query = new GetRoleListQuery();

        var result = await Mediator.Send(query, cancellationToken);

        return result.Match(
            result => Ok(result),
            errors => Problem(errors));
    }
}
