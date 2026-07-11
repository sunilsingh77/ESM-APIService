namespace EmployeeSkills.Application.DTOs.EmployeeSkill;

public class CreateEmployeeSkillDto
{
    public int EmployeeId { get; set; }
    public int SkillId { get; set; }
    public string ProficiencyLevel { get; set; }
    public int YearsOfExperience { get; set; }
    public bool IsPrimary { get; set; }
}

