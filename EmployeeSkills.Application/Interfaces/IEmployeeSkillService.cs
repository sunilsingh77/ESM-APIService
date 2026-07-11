using EmployeeSkills.Application.DTOs.EmployeeSkill;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeSkills.Application.Interfaces;
public interface IEmployeeSkillService
{
    Task<IEnumerable<EmployeeSkillDto>> GetAllAsync();
    Task<EmployeeSkillDto?> GetByIdAsync(int id);
    Task<IEnumerable<EmployeeSkillDto>> GetByEmployeeIdAsync(int employeeId);
    Task<int> CreateAsync(CreateEmployeeSkillDto dto);
    Task UpdateAsync(int id, UpdateEmployeeSkillDto dto);
    Task DeleteAsync(int id);
}
