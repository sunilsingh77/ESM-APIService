using EmployeeSkills.Application.Departments.DTOs;
using EmployeeSkills.Application.Departments.Interfaces;
using EmployeeSkills.Domain.Repositories;
using EmployeeSkillsSummary.Domain.Entities;

namespace EmployeeSkills.Application.Departments.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        public DepartmentService(IDepartmentRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
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
            await _unitOfWork.SaveChangesAsync();
            return department.Id;
        }
        public async Task UpdateAsync(int id, UpdateDepartmentDto dto)
        {
            var department = await _repository.GetByIdAsync(id);

            if (department == null)
                throw new Exception("Department not found.");

            department.Update(dto.Name, dto.Description);
            await _repository.UpdateAsync(department);
            await _unitOfWork.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var department = await _repository.GetByIdAsync(id);
            if (department == null)
                throw new Exception("Department not found.");

            await _repository.DeleteAsync(department);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
