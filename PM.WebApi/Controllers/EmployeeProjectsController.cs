using Microsoft.AspNetCore.Mvc;
using PM.Application.Features.EmployeeProjectsContext.Commands.AddEmployeeToProject;
using PM.Application.Features.EmployeeProjectsContext.Commands.RemoveEmployeeFromProject;
using PM.Application.Features.EmployeeProjectsContext.Dtos;

namespace PM.WebApi.Controllers
{
    public class EmployeeProjectsController : ApiBaseController
    {
        [HttpPost]
        [ProducesResponseType(typeof(AddEmployeeToProjectResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> AddEmployeeToProjectAsync(
            AddEmployeeToProjectCommand command,
            CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(command, cancellationToken);

            return result.Match(
                result => Ok(result),
                errors => Problem(errors));
        }

        [HttpDelete("{projectId}/{employeeId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> RemoveEmployeeToProjectAsync(
            int projectId,
            int employeeId,
            CancellationToken cancellationToken)
        {
            var command = new RemoveEmployeeFromProjectCommand()
            {
                ProjectId = projectId,
                EmployeeId = employeeId
            };

            var result = await Mediator.Send(command, cancellationToken);

            return result.Match(
                result => NoContent(),
                errors => Problem(errors));
        }
    }
}
