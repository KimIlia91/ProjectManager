using Microsoft.AspNetCore.Mvc;
using PM.Application.Features.EmployeeProjectsContext.Commands.AddEmployeeToProject;
using PM.Application.Features.EmployeeProjectsContext.Commands.RemoveEmployeeFromProject;
using PM.Contracts.EmployeeProjectsContracts.Requests;
using PM.Contracts.EmployeeProjectsContracts.Responses;

namespace PM.WebApi.Controllers
{
    public class EmployeeProjectsController : BaseController
    {
        [HttpPost]
        [ProducesResponseType(typeof(AddEmployeeToProjectResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> AddEmployeeToProjectAsync(
            AddEmployeeToProjectRequest request,
            CancellationToken cancellationToken)
        {
            var command = Mapper.Map<AddEmployeeToProjectCommand>(request);
            var result = await Mediator.Send(command, cancellationToken);

            return result.Match(
                result => Ok(Mapper.Map<AddEmployeeToProjectResponse>(result)),
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
                ProjectId = projectId, EmployeeId = employeeId 
            };

            var result = await Mediator.Send(command, cancellationToken);

            return result.Match(
                result => NoContent(),
                errors => Problem(errors));
        }
    }
}
