using System.ComponentModel.DataAnnotations;

namespace EmployeeSkills.Application.Authentication.DTOs
{
    public class UpdateUserRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        public IList<string> Roles { get; set; } = new List<string>();
    }
}
