namespace EmployeeSkills.Domain.Entities
{
    public class EmployeeSkill
    {
        public int Id { get; set; }

        // Foreign Keys
        public int EmployeeId { get; set; }
        public int SkillId { get; set; }

        // Skill Level (e.g., Beginner, Intermediate, Advanced, Expert)
        public string ProficiencyLevel { get; set; }

        // Years of experience with this skill
        public int YearsOfExperience { get; set; }

        // Whether this is a primary skill for the employee
        public bool IsPrimary { get; set; } = false;

        public DateTime AcquiredDate { get; set; } = DateTime.UtcNow;
        public DateTime? LastUpdatedDate { get; set; }

        // Navigation properties
        public Employee Employee { get; set; }
        public Skill Skill { get; set; }
    }
}
