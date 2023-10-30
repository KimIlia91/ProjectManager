using Microsoft.AspNetCore.Mvc;

namespace PM.WebApi.Controllers;

public class EmployeeController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> CreateEmployeeAsync()
    {


        return Ok();
    }
}
