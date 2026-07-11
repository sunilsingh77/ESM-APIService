using EmployeeSkills.Application.DTOs.Skill;

namespace EmployeeSkills.Application.Interfaces
{
    public interface ISkillService
    {
        Task<IEnumerable<SkillDto>> GetAllAsync();
        Task<SkillDto?> GetByIdAsync(int id);
        Task<int> CreateAsync(CreateSkillDto dto);
        Task UpdateAsync(int id, UpdateSkillDto dto);
        Task DeleteAsync(int id);
    }
}
