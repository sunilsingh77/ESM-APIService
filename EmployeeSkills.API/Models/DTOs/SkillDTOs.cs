namespace EmployeeSkills.API.Models.DTOs
{
    public class SkillDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
    }

    public class CreateSkillDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
    }

    public class UpdateSkillDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
    }
}
