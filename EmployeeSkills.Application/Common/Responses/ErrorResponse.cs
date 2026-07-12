namespace EmployeeSkills.Application.Common.Responses;

public record ErrorResponse
{
    public bool Success => false;
    public int StatusCode { get; init; }
    public string Message { get; init; } = string.Empty;
    public string? ErrorCode { get; init; }
    public string? TraceId { get; init; }
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}