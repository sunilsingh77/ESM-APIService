using EmployeeSkills.Application.Departments.Services;
using EmployeeSkills.Application.EmployeeSkills.Services;
using EmployeeSkills.Application.Skills.DTOs;
using EmployeeSkills.Application.Skills.Interfaces;
using EmployeeSkills.Domain.Entities;
using EmployeeSkills.Domain.Repositories;
using Microsoft.Extensions.Logging;

namespace EmployeeSkills.Application.Skills.Services
{
    public class SkillService : ISkillService
    {
        private readonly ISkillRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<SkillService> _logger;
        public SkillService(ISkillRepository repository, IUnitOfWork unitOfWork, ILogger<SkillService> logger)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _logger = logger;
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

            _logger.LogInformation($"Creating Skill - Skill Name {dto.Name}");
            await _unitOfWork.SaveChangesAsync();
            _logger.LogInformation($"Skill - {skill.Id} created successfully.");

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

            _logger.LogInformation($"Updating Skill - {id}");
            await _unitOfWork.SaveChangesAsync();
            _logger.LogInformation($"Skill Id - {skill.Id} updated successfully.");
        }
        public async Task DeleteAsync(int id)
        {
            var skill = await _repository.GetByIdAsync(id);
            if (skill == null)
                throw new Exception("Skill not found.");
            await _repository.DeleteAsync(skill);

            _logger.LogInformation($"Deletting Skill - {id}");
            await _unitOfWork.SaveChangesAsync();
            _logger.LogInformation($"SkillId - {skill.Id} deleted successfully.");
        }
    }
}
