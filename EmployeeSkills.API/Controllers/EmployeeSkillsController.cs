using EmployeeSkills.Application.DTOs.EmployeeSkill;
using EmployeeSkills.Application.Interfaces;
using EmployeeSkills.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeSkills.API.Controllers;

[ApiController]
[Route("api/[controller]")]
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

/*{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeSkillsController : ControllerBase
    {
        private readonly EmployeeSkillsDbContext _context;

        public EmployeeSkillsController(EmployeeSkillsDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeSkillDto>>> GetEmployeeSkills()
        {
            try
            {
                var employeeSkills = await _context.EmployeeSkills
                    .Include(es => es.Skill)
                    .Select(es => new EmployeeSkillDto
                    {
                        Id = es.Id,
                        EmployeeId = es.EmployeeId,
                        EmployeeName = $"{es.Employee.FirstName} {es.Employee.LastName}",
                        SkillId = es.SkillId,
                        SkillName = es.Skill.Name,
                        ProficiencyLevel = es.ProficiencyLevel,
                        YearsOfExperience = es.YearsOfExperience,
                        IsPrimary = es.IsPrimary,
                        AcquiredDate = es.AcquiredDate
                    })
                    .ToListAsync();

                return Ok(employeeSkills);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { title = "Server error", message = ex.Message, status = 500 });
            }
            finally { }
        }

        [HttpGet("employee/{employeeId}")]
        public async Task<ActionResult<IEnumerable<EmployeeSkillDto>>> GetEmployeeSkillsByEmployee(int employeeId)
        {
            try
            {
                var employee = await _context.Employees.FindAsync(employeeId);
                if (employee == null)
                    return NotFound("Employee not found");

                var skills = await _context.EmployeeSkills
                    .Where(es => es.EmployeeId == employeeId)
                    .Include(es => es.Skill)
                    .Select(es => new EmployeeSkillDto
                    {
                        Id = es.Id,
                        EmployeeId = es.EmployeeId,
                        SkillId = es.SkillId,
                        SkillName = es.Skill.Name,
                        ProficiencyLevel = es.ProficiencyLevel,
                        YearsOfExperience = es.YearsOfExperience,
                        IsPrimary = es.IsPrimary,
                        AcquiredDate = es.AcquiredDate
                    })
                    .ToListAsync();

                return Ok(skills);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { title = "Server error", message = ex.Message, status = 500 });
            }
            finally { }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeSkillDto>> GetEmployeeSkill(int id)
        {
            try
            {
                var employeeSkill = await _context.EmployeeSkills
                    .Include(es => es.Skill)
                    .FirstOrDefaultAsync(es => es.Id == id);

                if (employeeSkill == null)
                    return NotFound();

                return Ok(new EmployeeSkillDto
                {
                    Id = employeeSkill.Id,
                    EmployeeId = employeeSkill.EmployeeId,
                    SkillId = employeeSkill.SkillId,
                    SkillName = employeeSkill.Skill.Name,
                    ProficiencyLevel = employeeSkill.ProficiencyLevel,
                    YearsOfExperience = employeeSkill.YearsOfExperience,
                    IsPrimary = employeeSkill.IsPrimary,
                    AcquiredDate = employeeSkill.AcquiredDate
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { title = "Server error", message = ex.Message, status = 500 });
            }
            finally { }
        }

        [HttpPost]
        public async Task<ActionResult<EmployeeSkillDto>> CreateEmployeeSkill([FromBody] CreateEmployeeSkillDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var employee = await _context.Employees.FindAsync(dto.EmployeeId);
                if (employee == null)
                    return BadRequest("Employee not found");

                var skill = await _context.Skills.FindAsync(dto.SkillId);
                if (skill == null)
                    return BadRequest("Skill not found");

                var existingSkill = await _context.EmployeeSkills
                    .FirstOrDefaultAsync(es => es.EmployeeId == dto.EmployeeId && es.SkillId == dto.SkillId);

                if (existingSkill != null)
                    return BadRequest("Employee already has this skill assigned");

                var employeeSkill = new EmployeeSkill
                {
                    EmployeeId = dto.EmployeeId,
                    SkillId = dto.SkillId,
                    ProficiencyLevel = dto.ProficiencyLevel,
                    YearsOfExperience = dto.YearsOfExperience,
                    IsPrimary = dto.IsPrimary
                };

                _context.EmployeeSkills.Add(employeeSkill);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetEmployeeSkill), new { id = employeeSkill.Id }, new EmployeeSkillDto
                {
                    Id = employeeSkill.Id,
                    EmployeeId = employeeSkill.EmployeeId,
                    SkillId = employeeSkill.SkillId,
                    SkillName = skill.Name,
                    ProficiencyLevel = employeeSkill.ProficiencyLevel,
                    YearsOfExperience = employeeSkill.YearsOfExperience,
                    IsPrimary = employeeSkill.IsPrimary,
                    AcquiredDate = employeeSkill.AcquiredDate
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { title = "Server error", message = ex.Message, status = 500 });
            }
            finally { }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployeeSkill(int id, [FromBody] UpdateEmployeeSkillDto dto)
        {
            try
            {
                var employeeSkill = await _context.EmployeeSkills.FindAsync(id);
                if (employeeSkill == null)
                    return NotFound();

                employeeSkill.ProficiencyLevel = dto.ProficiencyLevel;
                employeeSkill.YearsOfExperience = dto.YearsOfExperience;
                employeeSkill.IsPrimary = dto.IsPrimary;
                employeeSkill.LastUpdatedDate = DateTime.UtcNow;

                _context.EmployeeSkills.Update(employeeSkill);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { title = "Server error", message = ex.Message, status = 500 });
            }
            finally { }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployeeSkill(int id)
        {
            try
            {
                var employeeSkill = await _context.EmployeeSkills.FindAsync(id);
                if (employeeSkill == null)
                    return NotFound();

                _context.EmployeeSkills.Remove(employeeSkill);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { title = "Server error", message = ex.Message, status = 500 });
            }
            finally { }
        }
    }
}
*/