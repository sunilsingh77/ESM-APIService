namespace EmployeeSkills.Domain.Entities;

public class EmployeeSkill
{
    public int Id { get; private set; }
    public int EmployeeId { get; private set; }
    public int SkillId { get; private set; }
    public string ProficiencyLevel { get; private set; }
    public int YearsOfExperience { get; private set; }
    public bool IsPrimary { get; private set; }
    public DateTime AcquiredDate { get; private set; }
    public DateTime? LastUpdatedDate { get; private set; }
    public Employee Employee { get; private set; }
    public Skill Skill { get; private set; }
    private EmployeeSkill() { }
    public EmployeeSkill(
        int employeeId,
        int skillId,
        string proficiencyLevel,
        int yearsOfExperience,
        bool isPrimary)
    {
        EmployeeId = employeeId;
        SkillId = skillId;
        ProficiencyLevel = proficiencyLevel;
        YearsOfExperience = yearsOfExperience;
        IsPrimary = isPrimary;
        AcquiredDate = DateTime.UtcNow;
    }
    public void Update(
        string proficiencyLevel,
        int yearsOfExperience,
        bool isPrimary)
    {
        ProficiencyLevel = proficiencyLevel;
        YearsOfExperience = yearsOfExperience;
        IsPrimary = isPrimary;
        LastUpdatedDate = DateTime.UtcNow;
    }
}