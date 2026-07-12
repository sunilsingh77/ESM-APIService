using System.ComponentModel.DataAnnotations;

namespace EmployeeSkills.Application.Authentication.DTOs
{
    public class AssignRoleRequest
    {
        [Required]
        public string Role { get; set; } = string.Empty;
    }
}
