using EmployeeSkills.Domain.Entities;
using EmployeeSkills.Domain.Repositories;
using EmployeeSkills.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EmployeeSkills.Infrastructure.Repositories;
public class EmployeeSkillRepository : IEmployeeSkillRepository
{
    private readonly EmployeeSkillsDbContext _context;
    public EmployeeSkillRepository(EmployeeSkillsDbContext context)
    {
        _context = context;
    }
    public async Task<List<EmployeeSkill>> GetAllAsync()
    {
        return await _context.EmployeeSkills
            .Include(x => x.Employee)
            .Include(x => x.Skill)
            .AsNoTracking()
            .OrderBy(x => x.Employee.FirstName)
            .ToListAsync();
    }
    public async Task<EmployeeSkill?> GetByIdAsync(int id)
    {
        return await _context.EmployeeSkills
            .Include(x => x.Employee)
            .Include(x => x.Skill)
            .FirstOrDefaultAsync(x => x.Id == id);
    }
    public async Task<List<EmployeeSkill>> GetByEmployeeIdAsync(int employeeId)
    {
        return await _context.EmployeeSkills
            .Include(x => x.Skill)
            .Include(x => x.Employee)
            .Where(x => x.EmployeeId == employeeId)
            .AsNoTracking()
            .OrderBy(x => x.Skill.Name)
            .ToListAsync();
    }
    public async Task AddAsync(EmployeeSkill employeeSkill)
    {
        await _context.EmployeeSkills.AddAsync(employeeSkill);
    }
    public Task UpdateAsync(EmployeeSkill employeeSkill)
    {
        _context.EmployeeSkills.Update(employeeSkill);
        return Task.CompletedTask;
    }
    public Task DeleteAsync(EmployeeSkill employeeSkill)
    {
        _context.EmployeeSkills.Remove(employeeSkill);
        return Task.CompletedTask;
    }
    public async Task<bool> ExistsAsync(int employeeId, int skillId)
    {
        return await _context.EmployeeSkills
            .AnyAsync(x =>
                x.EmployeeId == employeeId &&
                x.SkillId == skillId);
    }
    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
