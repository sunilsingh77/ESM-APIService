using EmployeeSkills.Domain.Entities;
using EmployeeSkills.Domain.Repositories;
using EmployeeSkills.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeSkills.Infrastructure.Repositories
{
    public class SkillRepository : ISkillRepository
    {
        private readonly EmployeeSkillsDbContext _context;
        public SkillRepository(EmployeeSkillsDbContext context)
        {
            _context = context;
        }
        public async Task<List<Skill>> GetAllAsync()
        {
            return await _context.Skills
                .AsNoTracking()
                .OrderBy(x => x.Name)
                .ToListAsync();
        }
        public async Task<Skill?> GetByIdAsync(int id)
        {
            return await _context.Skills
                .FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task AddAsync(Skill skill)
        {
            await _context.Skills.AddAsync(skill);
        }
        public Task UpdateAsync(Skill skill)
        {
            _context.Skills.Update(skill);
            return Task.CompletedTask;
        }
        public Task DeleteAsync(Skill skill)
        {
            _context.Skills.Remove(skill);
            return Task.CompletedTask;
        }
        public async Task<bool> ExistsAsync(string name)
        {
            return await _context.Skills
                .AnyAsync(x => x.Name == name);
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
