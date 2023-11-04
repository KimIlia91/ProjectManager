using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PM.Application.Features.RoleContext.Commands.GetRoleList;
using PM.Application.Features.RoleContext.Dtos;
using PM.Domain.Common.Constants;

namespace PM.WebApi.Controllers;

/// <summary>
/// 
/// </summary>
public class RoleController : ApiBaseController
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
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
