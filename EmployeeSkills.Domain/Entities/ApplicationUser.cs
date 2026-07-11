using EmployeeSkills.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace EmployeeSkillsSummary.Domain.Entities;

public class ApplicationUser : IdentityUser
{
    public string FullName { get; private set; }
    public DateTime CreatedDate { get; private set; }
    public ICollection<RefreshToken> RefreshTokens { get; private set; } = new List<RefreshToken>();
    public ApplicationUser(){ }
    public ApplicationUser(string fullName, string email)
    {
        FullName = fullName;
        Email = email;
        UserName = email;
        CreatedDate = DateTime.UtcNow;
    }
}

