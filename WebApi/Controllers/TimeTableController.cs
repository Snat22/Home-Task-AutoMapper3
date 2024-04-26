using Domain.DTOs.TimeTableDtos;
using Domain.Filter;
using Domain.Responses;
using Infrastructure.Services.TimeTableServices;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

public class TimeTableController(ITimeTableSrvice service):ControllerBase
{
    [HttpGet("get-TimeTable")]
    public async Task<Response<List<GetTimeTableDto>>> GetTimeTableAsynccc([FromQuery] TimeTableFilter filter)
    {
        return await service.GetTimeTableFillter(filter);
    }
    [HttpGet("{TimeTableId:int}")]
    public async Task<Response<GetTimeTableDto>> GetTimeTableByIdAsync(int timeTableDTO)
    {
        return await service.GetTimeTableById(timeTableDTO);
    }
    
    [HttpPost("create-TimeTable")]
    public async Task<Response<string>> AddTimeTableAsync(AddTimeTableDto timeTableDTO)
    {
        return await service.AddTimeTable(timeTableDTO);
    }
    
    [HttpPut("update-TimeTable")]
    public async Task<Response<string>> UpdateTimeTableAsync(UpdateTimeTableDto timeTableDTO)
    {
        return await service.UpdateTimeTable(timeTableDTO);
    }
    
    [HttpDelete("{TimeTableId:int}")]
    public async Task<Response<bool>> DeleteStudentAsync(int TimeTableId)
    {
        return await service.DeleteTimeTable(TimeTableId);
    }
 
}

