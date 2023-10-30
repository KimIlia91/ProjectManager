using Microsoft.AspNetCore.Mvc;
using PM.Domain.Common.Enums;

namespace PM.WebApi.Controllers;

public class TaskController : BaseController
{
    [HttpGet]
    public async Task<IActionResult> GetTasksAsync()
    {
        return Ok(Priority.Low);
    }
}
