using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PM.Application.Features.StatusContext.Dtos;
using PM.Application.Features.StatusContext.Queries.GetStatusList;
using PM.Domain.Common.Constants;
using PM.Domain.Common.Extensions;

namespace PM.WebApi.Controllers;

/// <summary>
/// Controller for managing project statuses.
/// </summary>
public class StatusController : ApiBaseController
{
    /// <summary>
    /// Retrieve a list of project statuses.
    /// </summary>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>
    /// An IActionResult representing the list of project statuses, which can be one of the following:
    /// - 200 OK with the list of statuses if successful.
    /// - A problem response with errors if there are issues.
    /// </returns>
    [HttpGet]
    [ProducesResponseType(typeof(List<EnumResult>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetStatusListAsync(CancellationToken cancellationToken)
    {
        var query = new GetStatusListQuery();
        var result = await Mediator.Send(query, cancellationToken);

        return result.Match(
            result => Ok(result),
            errors => Problem(errors));
    }
}
