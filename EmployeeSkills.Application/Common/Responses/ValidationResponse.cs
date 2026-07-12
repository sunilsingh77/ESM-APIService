namespace EmployeeSkills.Application.Common.Responses;
public record ValidationResponse : ErrorResponse
{
    public List<ApiValidationError> Errors { get; init; } = new();
}