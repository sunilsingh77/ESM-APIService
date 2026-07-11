using EmployeeSkills.Application.DTOs.Department;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
