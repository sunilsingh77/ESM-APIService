using EmployeeSkills.Application.Skills.DTOs;
using EmployeeSkills.Application.Skills.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeSkills.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class SkillController : ControllerBase
{
    private readonly ISkillService _service;

    public SkillController(ISkillService service)
    {
        _service = service;
    }
    [HttpGet]
    public async Task<IActionResult> GetSkills()
    {
        return Ok(await _service.GetAllAsync());
    }
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetSkill(int id)
    {
        var skill = await _service.GetByIdAsync(id);
        if (skill == null)
            return NotFound();
        return Ok(skill);
    }
    [HttpPost]
    public async Task<IActionResult> CreateSkill(CreateSkillDto dto)
    {
        var id = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetSkill), new { id }, null);
    }
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateSkill(int id, UpdateSkillDto dto)
    {
        await _service.UpdateAsync(id, dto);

        return NoContent();
    }
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteSkill(int id)
    {
        await _service.DeleteAsync(id);

        return NoContent();
    }
}
