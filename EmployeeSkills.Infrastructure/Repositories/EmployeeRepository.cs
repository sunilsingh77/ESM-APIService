using EmployeeSkills.Domain.Entities;
using EmployeeSkills.Domain.Repositories;
using EmployeeSkills.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EmployeeSkills.Infrastructure.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly EmployeeSkillsDbContext _context;

        public EmployeeRepository(EmployeeSkillsDbContext context)
        {
            _context = context;
        }
        public async Task<List<Employee>> GetAllAsync()
        {
            return await _context.Employees
                .Include(x => x.Department)
                .AsNoTracking()
                .OrderBy(x => x.FirstName)
                .ToListAsync();
        }
        public async Task<Employee?> GetByIdAsync(int id)
        {
            return await _context.Employees
                .Include(x => x.Department)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task AddAsync(Employee employee)
        {
            await _context.Employees.AddAsync(employee);
        }
        public Task UpdateAsync(Employee employee)
        {
            _context.Employees.Update(employee);
            return Task.CompletedTask;
        }
        public Task DeleteAsync(Employee employee)
        {
            _context.Employees.Remove(employee);
            return Task.CompletedTask;
        }
        public async Task<bool> ExistsAsync(string email)
        {
            return await _context.Employees
                .AnyAsync(x => x.Email == email);
        }
        public async Task<bool> ExistsAsync(int employeeId)
        {
            return await _context.Employees
                .AnyAsync(x => x.Id == employeeId);
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
