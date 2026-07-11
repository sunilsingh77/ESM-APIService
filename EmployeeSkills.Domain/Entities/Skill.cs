namespace EmployeeSkills.Domain.Entities
{
    public class Skill
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string Category { get; private set; }
        public DateTime CreatedDate { get; private set; }
        public ICollection<EmployeeSkill> EmployeeSkills { get; private set; } = new List<EmployeeSkill>();
        private Skill(){ }
        public Skill(string name, string description, string category)
        {
            Name = name;
            Description = description;
            Category = category;
            CreatedDate = DateTime.UtcNow;
        }
        public void Update(string name, string description, string category)
        {
            Name = name;
            Description = description;
            Category = category;
        }
    }
}
