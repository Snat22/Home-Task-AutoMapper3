using Domain.Enums;

namespace Domain.Filter;

public class GroupFilter : PaginationFilter
{
    public Status Status { get; set; }
    public string? GroupName { get; set; }
}
