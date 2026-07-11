using System.ComponentModel.DataAnnotations;

namespace EmployeeSkills.Application.DTOs.Authentication
{
    public class RefreshTokenRequest
    {
        [Required]
        public string RefreshToken { get; set; } = string.Empty;
    }
}
