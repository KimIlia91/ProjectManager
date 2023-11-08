using Microsoft.AspNetCore.Mvc;
using PM.Application.Features.PriorityContext.Queries;
using PM.Domain.Common.Extensions;

namespace PM.WebApi.Controllers;

public class PriorityController : ApiBaseController
{
    [HttpGet]
    [ProducesResponseType(typeof(List<EnumResult>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetPrioritiesAsync(
        CancellationToken cancellationToken)
    {
        var query = new GetPrioritiesQuery();
        var result = await Mediator.Send(query, cancellationToken);

        return result.Match(
            result => Ok(result),
            errors => Problem(errors));
    }
}
