using System.ComponentModel.DataAnnotations;

namespace EmployeeSkills.Application.DTOs.Authentication
{
    public class AssignRoleRequest
    {
        [Required]
        public string Role { get; set; } = string.Empty;
    }
}
