using EmployeeSkills.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
