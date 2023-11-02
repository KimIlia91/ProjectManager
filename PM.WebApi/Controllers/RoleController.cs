using Microsoft.AspNetCore.Mvc;
using PM.Application.Features.RoleContext.Commands.GetRoleList;
using PM.Application.Features.RoleContext.Dtos;

namespace PM.WebApi.Controllers
{
    public class RoleController : ApiBaseController
    {
        [HttpGet]
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
}
