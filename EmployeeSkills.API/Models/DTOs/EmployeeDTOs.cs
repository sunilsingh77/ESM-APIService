namespace EmployeeSkills.API.Models.DTOs
{
    public class EmployeeDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Position { get; set; }
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public DateTime HireDate { get; set; }
        public List<EmployeeSkillDto> Skills { get; set; } = new List<EmployeeSkillDto>();
    }

    public class CreateEmployeeDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Position { get; set; }
        public int DepartmentId { get; set; }
        public DateTime HireDate { get; set; }
    }

    public class UpdateEmployeeDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Position { get; set; }
        public int DepartmentId { get; set; }
    }

    public class EmployeeSkillDto
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int SkillId { get; set; }
        public string SkillName { get; set; }
        public string ProficiencyLevel { get; set; }
        public int YearsOfExperience { get; set; }
        public bool IsPrimary { get; set; }
        public DateTime AcquiredDate { get; set; }
    }

    public class CreateEmployeeSkillDto
    {
        public int EmployeeId { get; set; }
        public int SkillId { get; set; }
        public string ProficiencyLevel { get; set; }
        public int YearsOfExperience { get; set; }
        public bool IsPrimary { get; set; } = false;
    }

    public class UpdateEmployeeSkillDto
    {
        public string ProficiencyLevel { get; set; }
        public int YearsOfExperience { get; set; }
        public bool IsPrimary { get; set; }
    }
}
