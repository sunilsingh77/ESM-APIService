namespace EmployeeSkills.Domain.Entities
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        // Navigation property
        public ICollection<Employee> Employees { get; set; } = new List<Employee>();
    }
}
