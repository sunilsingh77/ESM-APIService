using EmployeeSkills.Domain.Entities;
namespace EmployeeSkillsSummary.Domain.Entities;
public class Department
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public DateTime CreatedDate { get; private set; }
    public ICollection<Employee> Employees { get; private set; }
        = new List<Employee>();

    private Department()
    {
    }

    public Department(string name, string description)
    {
        Name = name;
        Description = description;
        CreatedDate = DateTime.UtcNow;
    }

    public void Update(string name, string description)
    {
        Name = name;
        Description = description;
    }
}