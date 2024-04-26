using Domain.Enums;

namespace Domain.DTOs.TimeTableDtos;

public class AddTimeTableDto
{
    
    public int Id { get; set; }
    public DayOfWeek DayOfWeek { get; set; }
    public TimeSpan FromTime { get; set; }
    public TimeSpan ToTime { get; set; }
    public DateTimeOffset CreatedDate { get; set; }
    public TimeTableType MyProperty { get; set; }
    public int GroupId { get; set; }
}
