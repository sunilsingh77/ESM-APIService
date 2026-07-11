using EmployeeSkills.Application.DTOs.Department;
using EmployeeSkills.Application.Interfaces;
using EmployeeSkills.Domain.Repositories;
using EmployeeSkillsSummary.Domain.Entities;

namespace EmployeeSkills.Application.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _repository;
        public DepartmentService(IDepartmentRepository repository)
        {
            _repository = repository;
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

            await _repository.SaveChangesAsync();

            return department.Id;
        }
        public async Task UpdateAsync(int id, UpdateDepartmentDto dto)
        {
            var department = await _repository.GetByIdAsync(id);

            if (department == null)
                throw new Exception("Department not found.");

            department.Update(dto.Name, dto.Description);

            await _repository.UpdateAsync(department);

            await _repository.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var department = await _repository.GetByIdAsync(id);

            if (department == null)
                throw new Exception("Department not found.");

            await _repository.DeleteAsync(department);

            await _repository.SaveChangesAsync();
        }
    }
}
