using EmployeeSkills.Domain.Repositories;
using EmployeeSkills.Infrastructure.Persistence;
using EmployeeSkillsSummary.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmployeeSkills.Infrastructure.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly EmployeeSkillsDbContext _context;
        public DepartmentRepository(EmployeeSkillsDbContext context)
        {
            _context = context;
        }
        public async Task<List<Department>> GetAllAsync()
        {
            return await _context.Departments
                .AsNoTracking()
                .OrderBy(x => x.Name)
                .ToListAsync();
        }
        public async Task<Department?> GetByIdAsync(int id)
        {
            return await _context.Departments
                .FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task AddAsync(Department department)
        {
            await _context.Departments.AddAsync(department);
        }
        public Task UpdateAsync(Department department)
        {
            _context.Departments.Update(department);
            return Task.CompletedTask;
        }
        public Task DeleteAsync(Department department)
        {
            _context.Departments.Remove(department);
            return Task.CompletedTask;
        }
        public async Task<bool> ExistsAsync(string name)
        {
            return await _context.Departments
                .AnyAsync(x => x.Name == name);
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
