using EmployeeSkillsSummary.Domain.Entities;

namespace EmployeeSkills.Domain.Repositories
{
    public interface IDepartmentRepository
    {
        Task<List<Department>> GetAllAsync();
        Task<Department?> GetByIdAsync(int id);
        Task AddAsync(Department department);
        Task UpdateAsync(Department department);
        Task DeleteAsync(Department department);
        Task<bool> ExistsAsync(string name);
        Task SaveChangesAsync();
    }
}
