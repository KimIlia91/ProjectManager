using Microsoft.AspNetCore.Mvc;
using PM.Application.Features.StatusContext.Queries.GetStatusList;

namespace PM.WebApi.Controllers;

public class StatusController : ApiBaseController
{
    [HttpGet]
    public async Task<IActionResult> GetStatusListAsync(CancellationToken cancellationToken)
    {
        var query = new GetStatusListQuery();
        var result = await Mediator.Send(query, cancellationToken);

        return result.Match(
            result => Ok(result),
            errors => Problem(errors));
    }
}
