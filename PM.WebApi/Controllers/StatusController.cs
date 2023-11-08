using Microsoft.AspNetCore.Mvc;
using PM.Application.Features.StatusContext.Queries.GetStatusList;
using PM.Domain.Common.Extensions;

namespace PM.WebApi.Controllers;

/// <summary>
/// Controller for managing status information.
/// </summary>
public class StatusController : ApiBaseController
{
    /// <summary>
    /// Retrieves a list of status information asynchronously.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token for the operation.</param>
    /// <returns>An IActionResult containing a list of status information or an error response.</returns>
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
