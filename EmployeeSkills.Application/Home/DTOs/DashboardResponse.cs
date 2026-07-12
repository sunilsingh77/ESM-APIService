namespace EmployeeSkills.Application.Home.DTOs;

public class DashboardResponse
{
    public string UserName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public IList<string> Roles { get; set; } = new List<string>();

    public string Message { get; set; } = string.Empty;
}