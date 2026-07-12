namespace EmployeeSkills.Application.Common.Responses;

public class PagedResponse<T> : ApiResponse<IEnumerable<T>>
{
    public int PageNumber { get; init; }
    public int PageSize { get; init; }
    public int TotalRecords { get; init; }
    public int TotalPages => (int)Math.Ceiling((double)TotalRecords / PageSize);
}