using Domain.Enums;

namespace Domain.Filter;

public class MentorFilter : PaginationFilter
{
    public string? Address { get; set; }
    public Status Status { get; set; }
    public Gender Gender { get; set; }
    public string? DoB { get; set; }
}
