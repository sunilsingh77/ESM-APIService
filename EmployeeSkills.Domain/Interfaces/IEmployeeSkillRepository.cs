using EmployeeSkills.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeSkills.Domain.Repositories;
public interface IEmployeeSkillRepository
{
    Task<List<EmployeeSkill>> GetAllAsync();
    Task<EmployeeSkill?> GetByIdAsync(int id);
    Task<List<EmployeeSkill>> GetByEmployeeIdAsync(int employeeId);
    Task AddAsync(EmployeeSkill employeeSkill);
    Task UpdateAsync(EmployeeSkill employeeSkill);
    Task DeleteAsync(EmployeeSkill employeeSkill);
    Task<bool> ExistsAsync(int employeeId, int skillId);
    Task SaveChangesAsync();
}