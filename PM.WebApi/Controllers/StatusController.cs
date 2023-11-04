using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PM.Application.Features.StatusContext.Dtos;
using PM.Application.Features.StatusContext.Queries.GetStatusList;
using PM.Domain.Common.Constants;

namespace PM.WebApi.Controllers;

/// <summary>
/// 
/// </summary>
public class StatusController : ApiBaseController
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet]
    [Authorize(Roles = RoleConstants.Supervisor)]
    [ProducesResponseType(typeof(List<GetStatusListResult>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetStatusListAsync(CancellationToken cancellationToken)
    {
        var query = new GetStatusListQuery();
        var result = await Mediator.Send(query, cancellationToken);

        return result.Match(
            result => Ok(result),
            errors => Problem(errors));
    }
}
