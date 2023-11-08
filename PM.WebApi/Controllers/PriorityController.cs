using Microsoft.AspNetCore.Mvc;
using PM.Application.Features.PriorityContext.Queries;
using PM.Domain.Common.Extensions;

namespace PM.WebApi.Controllers;

/// <summary>
/// Controller for managing priorities.
/// </summary>
public class PriorityController : ApiBaseController
{
    /// <summary>
    /// Retrieves a list of priorities asynchronously.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token for the operation.</param>
    /// <returns>An IActionResult containing a list of priorities or an error response.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(List<EnumResult>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetPrioritiesAsync(CancellationToken cancellationToken)
    {
        var query = new GetPrioritiesQuery();
        var result = await Mediator.Send(query, cancellationToken);

        return result.Match(
            result => Ok(result),
            errors => Problem(errors));
    }
}