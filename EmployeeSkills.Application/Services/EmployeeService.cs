using EmployeeSkills.Application.DTOs.Employee;
using EmployeeSkills.Application.Interfaces;
using EmployeeSkills.Domain.Entities;
using EmployeeSkills.Domain.Repositories;

namespace EmployeeSkills.Application.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _repository;

        public EmployeeService(IEmployeeRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<EmployeeDto>> GetAllAsync()
        {
            var employees = await _repository.GetAllAsync();

            return employees.Select(x => new EmployeeDto
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Email = x.Email,
                PhoneNumber = x.PhoneNumber,
                Position = x.Position,
                DepartmentId = x.DepartmentId,
                DepartmentName = x.Department?.Name,
                HireDate = x.HireDate,
                CreatedDate = x.CreatedDate
            });
        }

        public async Task<EmployeeDto?> GetByIdAsync(int id)
        {
            var employee = await _repository.GetByIdAsync(id);

            if (employee == null)
                return null;

            return new EmployeeDto
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Email = employee.Email,
                PhoneNumber = employee.PhoneNumber,
                Position = employee.Position,
                DepartmentId = employee.DepartmentId,
                DepartmentName = employee.Department?.Name,
                HireDate = employee.HireDate,
                CreatedDate = employee.CreatedDate
            };
        }

        public async Task<int> CreateAsync(CreateEmployeeDto dto)
        {
            if (await _repository.ExistsAsync(dto.Email))
                throw new Exception("Employee already exists.");

            var employee = new Employee(
                dto.FirstName,
                dto.LastName,
                dto.Email,
                dto.PhoneNumber,
                dto.Position,
                dto.DepartmentId,
                dto.HireDate);

            await _repository.AddAsync(employee);
            await _repository.SaveChangesAsync();

            return employee.Id;
        }

        public async Task UpdateAsync(int id, UpdateEmployeeDto dto)
        {
            var employee = await _repository.GetByIdAsync(id);

            if (employee == null)
                throw new Exception("Employee not found.");

            employee.Update(
                dto.FirstName,
                dto.LastName,
                dto.Email,
                dto.PhoneNumber,
                dto.Position,
                dto.DepartmentId,
                dto.HireDate);

            await _repository.UpdateAsync(employee);
            await _repository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var employee = await _repository.GetByIdAsync(id);

            if (employee == null)
                throw new Exception("Employee not found.");

            await _repository.DeleteAsync(employee);
            await _repository.SaveChangesAsync();
        }
    }
}
