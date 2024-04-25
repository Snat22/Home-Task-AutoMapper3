using Domain.Enums;

namespace Domain.Filter;

public class CourseFilter : PaginationFilter
{
    public Status Status { get; set; }
    public string? CourseName { get; set; }
}
