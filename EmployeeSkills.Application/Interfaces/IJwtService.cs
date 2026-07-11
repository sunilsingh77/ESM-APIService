using EmployeeSkillsSummary.Domain.Entities;

namespace EmployeeSkills.Application.Interfaces;
public interface IJwtService
{
    string GenerateAccessToken(ApplicationUser user, IList<string> roles);
    string GenerateRefreshToken();
}
