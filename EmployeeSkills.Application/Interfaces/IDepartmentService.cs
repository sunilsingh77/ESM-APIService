using EmployeeSkills.Application.DTOs.Department;

namespace EmployeeSkills.Application.Interfaces
{
    public interface IDepartmentService
    {
        Task<IEnumerable<DepartmentDto>> GetAllAsync();
        Task<DepartmentDto?> GetByIdAsync(int id);
        Task<int> CreateAsync(CreateDepartmentDto dto);
        Task UpdateAsync(int id, UpdateDepartmentDto dto);
        Task DeleteAsync(int id);
    }
}
