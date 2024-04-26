using Domain.Enums;

namespace Domain.Entities;

public class TimeTable:BaseEntity
{
    public DayOfWeek DayOfWeek { get; set; } = new DayOfWeek();
    public TimeSpan FromTime { get; set; } = TimeSpan.FromDays(2);
    public TimeSpan ToTime { get; set; } = TimeSpan.FromSeconds(2);
    public DateTimeOffset CreatedDate { get; set; }
    public TimeTableType TimeTableType { get; set; }
    public int GroupId { get; set; }
    public virtual Group? Group { get; set; }
}
