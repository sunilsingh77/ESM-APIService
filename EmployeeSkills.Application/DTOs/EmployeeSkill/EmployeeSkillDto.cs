namespace EmployeeSkills.Application.DTOs.EmployeeSkill
{
    public class EmployeeSkillDto
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public int SkillId { get; set; }
        public string SkillName { get; set; }
        public string ProficiencyLevel { get; set; }
        public int YearsOfExperience { get; set; }
        public bool IsPrimary { get; set; }
        public DateTime AcquiredDate { get; set; }
    }
}
