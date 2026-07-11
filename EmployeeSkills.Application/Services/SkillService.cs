using EmployeeSkills.Application.DTOs.Skill;
using EmployeeSkills.Application.Interfaces;
using EmployeeSkills.Domain.Entities;
using EmployeeSkills.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeSkills.Application.Services
{
    public class SkillService : ISkillService
    {
        private readonly ISkillRepository _repository;
        public SkillService(ISkillRepository repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<SkillDto>> GetAllAsync()
        {
            var skills = await _repository.GetAllAsync();

            return skills.Select(x => new SkillDto
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Category = x.Category,
                CreatedDate = x.CreatedDate
            });
        }
        public async Task<SkillDto?> GetByIdAsync(int id)
        {
            var skill = await _repository.GetByIdAsync(id);

            if (skill == null)
                return null;

            return new SkillDto
            {
                Id = skill.Id,
                Name = skill.Name,
                Description = skill.Description,
                Category = skill.Category,
                CreatedDate = skill.CreatedDate
            };
        }
        public async Task<int> CreateAsync(CreateSkillDto dto)
        {
            if (await _repository.ExistsAsync(dto.Name))
                throw new Exception("Skill already exists.");

            var skill = new Skill(
                dto.Name,
                dto.Description,
                dto.Category);

            await _repository.AddAsync(skill);

            await _repository.SaveChangesAsync();

            return skill.Id;
        }
        public async Task UpdateAsync(int id, UpdateSkillDto dto)
        {
            var skill = await _repository.GetByIdAsync(id);

            if (skill == null)
                throw new Exception("Skill not found.");

            skill.Update(
                dto.Name,
                dto.Description,
                dto.Category);

            await _repository.UpdateAsync(skill);

            await _repository.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var skill = await _repository.GetByIdAsync(id);

            if (skill == null)
                throw new Exception("Skill not found.");

            await _repository.DeleteAsync(skill);

            await _repository.SaveChangesAsync();
        }
    }
}
