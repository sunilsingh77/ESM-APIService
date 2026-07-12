using EmployeeSkills.Application.Home.DTOs;
using System.Security.Claims;

namespace EmployeeSkills.Application.Home.Interfaces;

public interface IHomeService
{
    Task<DashboardResponse> GetDashboardAsync(ClaimsPrincipal principal);
}