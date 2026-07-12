using EmployeeSkills.Application.EmployeeSkills.DTOs;
using EmployeeSkills.Application.EmployeeSkills.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeSkills.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class EmployeeSkillController : ControllerBase
{
    private readonly IEmployeeSkillService _service;

    public EmployeeSkillController(IEmployeeSkillService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetEmployeeSkills()
    {
        return Ok(await _service.GetAllAsync());
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetEmployeeSkill(int id)
    {
        var employeeSkill = await _service.GetByIdAsync(id);

        if (employeeSkill == null)
            return NotFound();

        return Ok(employeeSkill);
    }
    
    [HttpGet("employee/{employeeId:int}")]
    public async Task<IActionResult> GetEmployeeSkillsByEmployee(int employeeId)
    {
        var result = await _service.GetByEmployeeIdAsync(employeeId);
        return Ok(result);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateEmployeeSkill(CreateEmployeeSkillDto dto)
    {
        var id = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetEmployeeSkill), new { id }, null);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateEmployeeSkill(int id, UpdateEmployeeSkillDto dto)
    {
        await _service.UpdateAsync(id, dto);

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteEmployeeSkill(int id)
    {
        await _service.DeleteAsync(id);

        return NoContent();
    }
}