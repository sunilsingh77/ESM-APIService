using EmployeeSkills.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeSkills.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class HomeController : ControllerBase
{
    private readonly IHomeService _service;

    public HomeController(IHomeService service)
    {
        _service = service;
    }

    [HttpGet("dashboard")]
    public async Task<IActionResult> Dashboard()
    {
        return Ok(await _service.GetDashboardAsync(User));
    }

    [HttpGet("admin-only")]
    [Authorize(Roles = "Admin")]
    public IActionResult AdminOnly()
    {
        return Ok(new
        {
            Message = "Admin access granted"
        });
    }

    [HttpGet("manager-or-admin")]
    [Authorize(Roles = "Manager,Admin")]
    public IActionResult ManagerOrAdmin()
    {
        return Ok(new
        {
            Message = "Manager or Admin access granted"
        });
    }

    [HttpGet("employee")]
    [Authorize(Roles = "Employee,Manager,Admin")]
    public IActionResult Employee()
    {
        return Ok(new
        {
            Message = "Employee access granted"
        });
    }
}