using Domain.Enums;

namespace Domain.DTOs.TimeTableDtos;

public class UpdateTimeTableDto
{
    
    public int Id { get; set; }
    public DayOfWeek DayOfWeek { get; set; }
    public TimeSpan FromTime { get; set; }
    public TimeSpan ToTime { get; set; }
    public DateTimeOffset CreatedDate { get; set; }
    public TimeTableType TimeTableType { get; set; }
    public int GroupId { get; set; }
}
