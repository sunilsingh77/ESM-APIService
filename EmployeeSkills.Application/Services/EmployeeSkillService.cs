using EmployeeSkills.Application.DTOs.EmployeeSkill;
using EmployeeSkills.Application.Interfaces;
using EmployeeSkills.Domain.Entities;
using EmployeeSkills.Domain.Repositories;

namespace EmployeeSkills.Application.Services;
public class EmployeeSkillService : IEmployeeSkillService
{
    private readonly IEmployeeSkillRepository _employeeSkillRepository;
    private readonly IEmployeeRepository _employeeRepository;
    public EmployeeSkillService(IEmployeeSkillRepository employeeSkillRepository, IEmployeeRepository employeeRepository)
    {
        _employeeSkillRepository = employeeSkillRepository;
        _employeeRepository = employeeRepository;
    }
    public async Task<IEnumerable<EmployeeSkillDto>> GetAllAsync()
    {
        var list = await _employeeSkillRepository.GetAllAsync();

        return list.Select(x => new EmployeeSkillDto
        {
            Id = x.Id,
            EmployeeId = x.EmployeeId,
            EmployeeName = $"{x.Employee.FirstName} {x.Employee.LastName}",
            SkillId = x.SkillId,
            SkillName = x.Skill.Name,
            ProficiencyLevel = x.ProficiencyLevel,
            YearsOfExperience = x.YearsOfExperience,
            IsPrimary = x.IsPrimary,
            AcquiredDate = x.AcquiredDate
        });
    }
    public async Task<EmployeeSkillDto?> GetByIdAsync(int id)
    {
        var item = await _employeeSkillRepository.GetByIdAsync(id);

        if (item == null)
            return null;

        return new EmployeeSkillDto
        {
            Id = item.Id,
            EmployeeId = item.EmployeeId,
            EmployeeName = $"{item.Employee.FirstName} {item.Employee.LastName}",
            SkillId = item.SkillId,
            SkillName = item.Skill.Name,
            ProficiencyLevel = item.ProficiencyLevel,
            YearsOfExperience = item.YearsOfExperience,
            IsPrimary = item.IsPrimary,
            AcquiredDate = item.AcquiredDate
        };
    }
    public async Task<IEnumerable<EmployeeSkillDto>> GetByEmployeeIdAsync(int employeeId)
    {
        if (!await _employeeRepository.ExistsAsync(employeeId))
            throw new Exception("Employee not found.");

        var skills = await _employeeSkillRepository.GetByEmployeeIdAsync(employeeId);

        return skills.Select(x => new EmployeeSkillDto
        {
            Id = x.Id,
            EmployeeId = x.EmployeeId,
            EmployeeName = $"{x.Employee.FirstName} {x.Employee.LastName}",
            SkillId = x.SkillId,
            SkillName = x.Skill.Name,
            ProficiencyLevel = x.ProficiencyLevel,
            YearsOfExperience = x.YearsOfExperience,
            IsPrimary = x.IsPrimary,
            AcquiredDate = x.AcquiredDate
        });
    }
    public async Task<int> CreateAsync(CreateEmployeeSkillDto dto)
    {
        if (await _employeeSkillRepository.ExistsAsync(dto.EmployeeId, dto.SkillId))
            throw new Exception("Employee already has this skill.");

        var employeeSkill = new EmployeeSkill(
            dto.EmployeeId,
            dto.SkillId,
            dto.ProficiencyLevel,
            dto.YearsOfExperience,
            dto.IsPrimary);

        await _employeeSkillRepository.AddAsync(employeeSkill);
        await _employeeSkillRepository.SaveChangesAsync();
        return employeeSkill.Id;
    }
    public async Task UpdateAsync(int id, UpdateEmployeeSkillDto dto)
    {
        var employeeSkill = await _employeeSkillRepository.GetByIdAsync(id);

        if (employeeSkill == null)
            throw new Exception("Employee skill not found.");

        employeeSkill.Update(
            dto.ProficiencyLevel,
            dto.YearsOfExperience,
            dto.IsPrimary);

        await _employeeSkillRepository.UpdateAsync(employeeSkill);

        await _employeeSkillRepository.SaveChangesAsync();
    }
    public async Task DeleteAsync(int id)
    {
        var employeeSkill = await _employeeSkillRepository.GetByIdAsync(id);

        if (employeeSkill == null)
            throw new Exception("Employee skill not found.");
        await _employeeSkillRepository.DeleteAsync(employeeSkill);
        await _employeeSkillRepository.SaveChangesAsync();
    }
}
