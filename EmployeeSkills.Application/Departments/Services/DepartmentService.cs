using EmployeeSkills.Application.Departments.DTOs;
using EmployeeSkills.Application.Departments.Interfaces;
using EmployeeSkills.Application.Employees.Services;
using EmployeeSkills.Domain.Entities;
using EmployeeSkills.Domain.Repositories;
using EmployeeSkillsSummary.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace EmployeeSkills.Application.Departments.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<DepartmentService> _logger;
        public DepartmentService(IDepartmentRepository repository, IUnitOfWork unitOfWork, ILogger<DepartmentService> logger)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        public async Task<IEnumerable<DepartmentDto>> GetAllAsync()
        {
            var departments = await _repository.GetAllAsync();

            return departments.Select(x => new DepartmentDto
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                CreatedDate = x.CreatedDate
            });
        }
        public async Task<DepartmentDto?> GetByIdAsync(int id)
        {
            var department = await _repository.GetByIdAsync(id);

            if (department == null)
                return null;

            return new DepartmentDto
            {
                Id = department.Id,
                Name = department.Name,
                Description = department.Description,
                CreatedDate = department.CreatedDate
            };
        }
        public async Task<int> CreateAsync(CreateDepartmentDto dto)
        {
            if (await _repository.ExistsAsync(dto.Name))
                throw new Exception("Department already exists.");

            var department = new Department(dto.Name, dto.Description);

            await _repository.AddAsync(department);

            _logger.LogInformation($"Creating Department - {dto.Name}");
            await _unitOfWork.SaveChangesAsync();
            _logger.LogInformation($"Department - {department.Id} created successfully.");

            return department.Id;
        }
        public async Task UpdateAsync(int id, UpdateDepartmentDto dto)
        {
            var department = await _repository.GetByIdAsync(id);

            if (department == null)
                throw new Exception("Department not found.");

            department.Update(dto.Name, dto.Description);
            await _repository.UpdateAsync(department);

            _logger.LogInformation($"Updating Department - {id}");
            await _unitOfWork.SaveChangesAsync();
            _logger.LogInformation($"DepartmentId - {department.Id} updated successfully.");
        }
        public async Task DeleteAsync(int id)
        {
            var department = await _repository.GetByIdAsync(id);
            if (department == null)
                throw new Exception("Department not found.");

            await _repository.DeleteAsync(department);

            _logger.LogInformation($"Deletting DepartmentId - {id}");
            await _unitOfWork.SaveChangesAsync();
            _logger.LogInformation($"DepartmentId - {department.Id} deleted successfully.");
        }
    }
}
