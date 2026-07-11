using EmployeeSkills.Application.DTOs.Home;
using System.Security.Claims;

namespace EmployeeSkills.Application.Interfaces;

public interface IHomeService
{
    Task<DashboardResponse> GetDashboardAsync(ClaimsPrincipal principal);
}