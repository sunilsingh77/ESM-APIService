using EmployeeSkillsSummary.Domain.Entities;

namespace EmployeeSkills.Domain.Entities;

public class Employee
{
    public int Id { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Email { get; private set; }
    public string PhoneNumber { get; private set; }
    public string Position { get; private set; }
    public int DepartmentId { get; private set; }
    public DateTime HireDate { get; private set; }
    public DateTime CreatedDate { get; private set; }
    public DateTime? UpdatedDate { get; private set; }
    public Department Department { get; private set; }
    public ICollection<EmployeeSkill> EmployeeSkills { get; private set; } = new List<EmployeeSkill>();

    private Employee()
    {
    }

    public Employee(
        string firstName,
        string lastName,
        string email,
        string phoneNumber,
        string position,
        int departmentId,
        DateTime hireDate)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PhoneNumber = phoneNumber;
        Position = position;
        DepartmentId = departmentId;
        HireDate = hireDate;
        CreatedDate = DateTime.UtcNow;
    }

    public void Update(
        string firstName,
        string lastName,
        string email,
        string phoneNumber,
        string position,
        int departmentId,
        DateTime hireDate)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PhoneNumber = phoneNumber;
        Position = position;
        DepartmentId = departmentId;
        HireDate = hireDate;
        UpdatedDate = DateTime.UtcNow;
    }
}