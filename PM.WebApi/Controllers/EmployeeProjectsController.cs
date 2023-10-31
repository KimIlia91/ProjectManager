using Microsoft.AspNetCore.Mvc;
using PM.Application.Features.EmployeeProjectsContext.Commands.AddEmployeeToProject;
using PM.Contracts.EmployeeProjectsContracts.Requests;
using PM.Contracts.EmployeeProjectsContracts.Responses;

namespace PM.WebApi.Controllers
{
    public class EmployeeProjectsController : BaseController
    {
        [HttpPost]
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
    }
}
