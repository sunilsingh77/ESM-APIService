namespace EmployeeSkills.Domain.Entities
{
    public class Skill
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        // Navigation property
        public ICollection<EmployeeSkill> EmployeeSkills { get; set; } = new List<EmployeeSkill>();
    }
}
