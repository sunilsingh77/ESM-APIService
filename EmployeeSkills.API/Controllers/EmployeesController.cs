
using EmployeeSkills.Application.DTOs.Employee;
using EmployeeSkills.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

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
}
