using System.ComponentModel.DataAnnotations;

namespace EmployeeSkills.Application.Authentication.DTOs
{
    public class RefreshTokenRequest
    {
        [Required]
        public string RefreshToken { get; set; } = string.Empty;
    }
}
