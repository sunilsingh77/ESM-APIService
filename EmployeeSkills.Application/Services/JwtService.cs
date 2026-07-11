using EmployeeSkills.Application.Interfaces;
using EmployeeSkillsSummary.Domain.Entities;

namespace EmployeeSkills.Application.Services;
public class JwtService : IJwtService
{
    public string GenerateAccessToken(ApplicationUser user, IList<string> roles)
    {
        throw new NotImplementedException();
    }

    public string GenerateRefreshToken()
    {
        throw new NotImplementedException();
    }
}

