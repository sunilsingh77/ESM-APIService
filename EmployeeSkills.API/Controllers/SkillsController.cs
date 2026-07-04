using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EmployeeSkills.API.Data;
using EmployeeSkills.API.Models.DTOs;
using EmployeeSkills.Domain.Entities;

namespace EmployeeSkills.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SkillsController : ControllerBase
    {
        private readonly EmployeeSkillsDbContext _context;

        public SkillsController(EmployeeSkillsDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SkillDto>>> GetSkills()
        {
            try
            {
                var skills = await _context.Skills
                    .Select(s => new SkillDto
                    {
                        Id = s.Id,
                        Name = s.Name,
                        Description = s.Description,
                        Category = s.Category
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
        public async Task<ActionResult<SkillDto>> GetSkill(int id)
        {
            try
            {
                var skill = await _context.Skills.FindAsync(id);
                if (skill == null)
                    return NotFound();

                return Ok(new SkillDto
                {
                    Id = skill.Id,
                    Name = skill.Name,
                    Description = skill.Description,
                    Category = skill.Category
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { title = "Server error", message = ex.Message, status = 500 });
            }
            finally { }
        }

        [HttpPost]
        public async Task<ActionResult<SkillDto>> CreateSkill([FromBody] CreateSkillDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var skill = new Skill
                {
                    Name = dto.Name,
                    Description = dto.Description,
                    Category = dto.Category
                };

                _context.Skills.Add(skill);
                await _context.SaveChangesAsync();

                var skillDto = new SkillDto
                {
                    Id = skill.Id,
                    Name = skill.Name,
                    Description = skill.Description,
                    Category = skill.Category
                };

                return CreatedAtAction(nameof(GetSkill), new { id = skill.Id }, skillDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { title = "Server error", message = ex.Message, status = 500 });
            }
            finally { }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSkill(int id, [FromBody] UpdateSkillDto dto)
        {
            try
            {
                var skill = await _context.Skills.FindAsync(id);
                if (skill == null)
                    return NotFound();

                skill.Name = dto.Name;
                skill.Description = dto.Description;
                skill.Category = dto.Category;

                _context.Skills.Update(skill);
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
        public async Task<IActionResult> DeleteSkill(int id)
        {
            try
            {
                var skill = await _context.Skills.FindAsync(id);
                if (skill == null)
                    return NotFound();

                _context.Skills.Remove(skill);
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
