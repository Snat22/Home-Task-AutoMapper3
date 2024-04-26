using Domain.Enums;

namespace Domain.Filter;

public class TimeTableFilter:PaginationFilter
{
    
    public DayOfWeek DayOfWeek { get; set; }
    public TimeSpan FromTime { get; set; }
    public TimeSpan ToTime { get; set; }
    public TimeTableType TimeTableType { get; set; }
    public DateTimeOffset CreatedDate { get; set; }

}
