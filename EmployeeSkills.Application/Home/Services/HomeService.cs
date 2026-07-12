using EmployeeSkills.Application.Home.DTOs;
using EmployeeSkills.Application.Home.Interfaces;
using EmployeeSkillsSummary.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace EmployeeSkills.Application.Home.Services;

public class HomeService : IHomeService
{
    private readonly UserManager<ApplicationUser> _userManager;

    public HomeService(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<DashboardResponse> GetDashboardAsync(ClaimsPrincipal principal)
    {
        var user = await _userManager.GetUserAsync(principal);

        if (user == null)
            throw new UnauthorizedAccessException();

        var roles = await _userManager.GetRolesAsync(user);

        return new DashboardResponse
        {
            UserName = user.UserName ?? string.Empty,

            Email = user.Email ?? string.Empty,

            Roles = roles,

            Message = $"Welcome {user.UserName}!"
        };
    }
}