namespace EmployeeSkills.Application.Common.Responses;

public class ApiValidationError
{
    public string Field { get; init; } = string.Empty;
    public string Message { get; init; } = string.Empty;
}