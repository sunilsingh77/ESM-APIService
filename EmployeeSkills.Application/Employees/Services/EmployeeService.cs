using EmployeeSkills.Application.Employees.DTOs;
using EmployeeSkills.Application.Employees.Interfaces;
using EmployeeSkills.Domain.Entities;
using EmployeeSkills.Domain.Repositories;
using Microsoft.Extensions.Logging;

namespace EmployeeSkills.Application.Employees.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<EmployeeService> _logger;
        public EmployeeService(IEmployeeRepository repository, IUnitOfWork unitOfWork, ILogger<EmployeeService> logger)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _logger = logger;
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

            _logger.LogInformation($"Creating EmployeeName - {dto.FirstName} {dto.LastName}");
            await _unitOfWork.SaveChangesAsync();
            _logger.LogInformation($"EmployeeId - {employee.Id} created successfully.");

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

            _logger.LogInformation($"Updating EmployeeId - {employee.Id}");
            await _unitOfWork.SaveChangesAsync();
            _logger.LogInformation($"EmployeeId - {employee.Id} updated successfully.");
        }

        public async Task DeleteAsync(int id)
        {
            var employee = await _repository.GetByIdAsync(id);

            if (employee == null)
                throw new Exception("Employee not found.");

            await _repository.DeleteAsync(employee);

            _logger.LogInformation($"Deletting EmployeeId - {id}");
            await _unitOfWork.SaveChangesAsync();
            _logger.LogInformation($"EmployeeId - {employee.Id} deleted successfully.");
        }
    }
}
