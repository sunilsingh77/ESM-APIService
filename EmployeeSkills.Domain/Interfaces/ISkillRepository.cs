using EmployeeSkills.Domain.Entities;

namespace EmployeeSkills.Domain.Repositories
{
    public interface ISkillRepository
    {
        Task<List<Skill>> GetAllAsync();
        Task<Skill?> GetByIdAsync(int id);
        Task AddAsync(Skill skill);
        Task UpdateAsync(Skill skill);
        Task DeleteAsync(Skill skill);
        Task<bool> ExistsAsync(string name);
        Task SaveChangesAsync();
    }
}
