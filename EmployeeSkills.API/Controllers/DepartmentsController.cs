using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EmployeeSkills.API.Data;
using EmployeeSkills.API.Models.DTOs;
using EmployeeSkills.Domain.Entities;

namespace EmployeeSkills.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentsController : ControllerBase
    {
        private readonly EmployeeSkillsDbContext _context;

        public DepartmentsController(EmployeeSkillsDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DepartmentDto>>> GetDepartments()
        {
            try
            {
                var departments = await _context.Departments
                    .Select(d => new DepartmentDto
                    {
                        Id = d.Id,
                        Name = d.Name,
                        Description = d.Description
                    })
                    .ToListAsync();

                return Ok(departments);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { title = "Server error", message = ex.Message, status = 500 });
            }
            finally { }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DepartmentDto>> GetDepartment(int id)
        {
            try
            {
                var department = await _context.Departments.FindAsync(id);
                if (department == null)
                    return NotFound();

                return Ok(new DepartmentDto
                {
                    Id = department.Id,
                    Name = department.Name,
                    Description = department.Description
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { title = "Server error", message = ex.Message, status = 500 });
            }
            finally { }
        }

        [HttpPost]
        public async Task<ActionResult<DepartmentDto>> CreateDepartment([FromBody] CreateDepartmentDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var department = new Department
                {
                    Name = dto.Name,
                    Description = dto.Description
                };

                _context.Departments.Add(department);
                await _context.SaveChangesAsync();

                var departmentDto = new DepartmentDto
                {
                    Id = department.Id,
                    Name = department.Name,
                    Description = department.Description
                };

                return CreatedAtAction(nameof(GetDepartment), new { id = department.Id }, departmentDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { title = "Server error", message = ex.Message, status = 500 });
            }
            finally { }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDepartment(int id, [FromBody] UpdateDepartmentDto dto)
        {
            try
            {
                var department = await _context.Departments.FindAsync(id);
                if (department == null)
                    return NotFound();

                department.Name = dto.Name;
                department.Description = dto.Description;

                _context.Departments.Update(department);
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
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            try
            {
                var department = await _context.Departments.FindAsync(id);
                if (department == null)
                    return NotFound();

                _context.Departments.Remove(department);
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
