using EmployeeSkills.Application.DTOs;
using EmployeeSkills.Application.DTOs.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeSkills.Application.Interfaces
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeDto>> GetAllAsync();
        Task<EmployeeDto?> GetByIdAsync(int id);
        Task<int> CreateAsync(CreateEmployeeDto dto);
        Task UpdateAsync(int id, UpdateEmployeeDto dto);
        Task DeleteAsync(int id);
    }
}
