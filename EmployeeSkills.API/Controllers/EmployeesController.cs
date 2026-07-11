
using EmployeeSkills.Application.DTOs.Employee;
using EmployeeSkills.Application.Interfaces;
using EmployeeSkills.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeSkills.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _service;
        public EmployeeController(IEmployeeService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<IActionResult> GetEmployees()
        {
            return Ok(await _service.GetAllAsync());
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetEmployee(int id)
        {
            var employee = await _service.GetByIdAsync(id);
            if (employee == null)
                return NotFound();
            return Ok(employee);
        }
        [HttpPost]
        public async Task<IActionResult> CreateEmployee(CreateEmployeeDto dto)
        {
            var id = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetEmployee), new { id }, null);
        }
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateEmployee(int id, UpdateEmployeeDto dto)
        {
            await _service.UpdateAsync(id, dto);

            return NoContent();
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            await _service.DeleteAsync(id);

            return NoContent();
        }
    }

    /*
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly EmployeeSkillsDbContext _context;

        public EmployeesController(EmployeeSkillsDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetEmployees()
        {
            try
            {
                var employees = await _context.Employees
                    .Include(e => e.Department)
                    .Include(e => e.EmployeeSkills)
                    .ThenInclude(es => es.Skill)
                    .Select(e => new EmployeeDto
                    {
                        Id = e.Id,
                        FirstName = e.FirstName,
                        LastName = e.LastName,
                        Email = e.Email,
                        PhoneNumber = e.PhoneNumber,
                        Position = e.Position,
                        DepartmentId = e.DepartmentId,
                        DepartmentName = e.Department.Name,
                        HireDate = e.HireDate,
                        Skills = e.EmployeeSkills.Select(es => new EmployeeSkillDto
                        {
                            Id = es.Id,
                            EmployeeId = es.EmployeeId,
                            SkillId = es.SkillId,
                            SkillName = es.Skill.Name,
                            ProficiencyLevel = es.ProficiencyLevel,
                            YearsOfExperience = es.YearsOfExperience,
                            IsPrimary = es.IsPrimary,
                            AcquiredDate = es.AcquiredDate
                        }).ToList()
                    })
                    .ToListAsync();

                return Ok(employees);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { title = "Server error", message = ex.Message, status = 500 });
            }
            finally { }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeDto>> GetEmployee(int id)
        {
            try
            {
                var employee = await _context.Employees
                    .Include(e => e.Department)
                    .Include(e => e.EmployeeSkills)
                    .ThenInclude(es => es.Skill)
                    .FirstOrDefaultAsync(e => e.Id == id);

                if (employee == null)
                    return NotFound();

                return Ok(new EmployeeDto
                {
                    Id = employee.Id,
                    FirstName = employee.FirstName,
                    LastName = employee.LastName,
                    Email = employee.Email,
                    PhoneNumber = employee.PhoneNumber,
                    Position = employee.Position,
                    DepartmentId = employee.DepartmentId,
                    DepartmentName = employee.Department.Name,
                    HireDate = employee.HireDate,
                    Skills = employee.EmployeeSkills.Select(es => new EmployeeSkillDto
                    {
                        Id = es.Id,
                        EmployeeId = es.EmployeeId,
                        SkillId = es.SkillId,
                        SkillName = es.Skill.Name,
                        ProficiencyLevel = es.ProficiencyLevel,
                        YearsOfExperience = es.YearsOfExperience,
                        IsPrimary = es.IsPrimary,
                        AcquiredDate = es.AcquiredDate
                    }).ToList()
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { title = "Server error", message = ex.Message, status = 500 });
            }
            finally { }
        }

        [HttpPost]
        public async Task<ActionResult<EmployeeDto>> CreateEmployee([FromBody] CreateEmployeeDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var department = await _context.Departments.FindAsync(dto.DepartmentId);
                if (department == null)
                    return BadRequest("Department not found");

                var employee = new Employee
                {
                    FirstName = dto.FirstName,
                    LastName = dto.LastName,
                    Email = dto.Email,
                    PhoneNumber = dto.PhoneNumber,
                    Position = dto.Position,
                    DepartmentId = dto.DepartmentId,
                    HireDate = dto.HireDate
                };

                _context.Employees.Add(employee);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetEmployee), new { id = employee.Id }, new EmployeeDto
                {
                    Id = employee.Id,
                    FirstName = employee.FirstName,
                    LastName = employee.LastName,
                    Email = employee.Email,
                    PhoneNumber = employee.PhoneNumber,
                    Position = employee.Position,
                    DepartmentId = employee.DepartmentId,
                    DepartmentName = department.Name,
                    HireDate = employee.HireDate
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { title = "Server error", message = ex.Message, status = 500 });
            }
            finally { }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(int id, [FromBody] UpdateEmployeeDto dto)
        {
            try
            {
                var employee = await _context.Employees.FindAsync(id);
                if (employee == null)
                    return NotFound();

                var department = await _context.Departments.FindAsync(dto.DepartmentId);
                if (department == null)
                    return BadRequest("Department not found");

                employee.FirstName = dto.FirstName;
                employee.LastName = dto.LastName;
                employee.Email = dto.Email;
                employee.PhoneNumber = dto.PhoneNumber;
                employee.Position = dto.Position;
                employee.DepartmentId = dto.DepartmentId;
                employee.UpdatedDate = DateTime.UtcNow;

                _context.Employees.Update(employee);
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
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            try
            {
                var employee = await _context.Employees.FindAsync(id);
                if (employee == null)
                    return NotFound();

                _context.Employees.Remove(employee);
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
    */
}
