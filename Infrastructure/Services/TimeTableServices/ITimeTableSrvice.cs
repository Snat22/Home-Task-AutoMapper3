using Domain.DTOs.TimeTableDtos;
using Domain.Filter;
using Domain.Responses;

namespace Infrastructure.Services.TimeTableServices;

public interface ITimeTableSrvice
{
    
    public Task<Response<string>> AddTimeTable(AddTimeTableDto addTime);
    public Task<PagedResponse<List<GetTimeTableDto>>> GetTimeTableFillter(TimeTableFilter filter);
    public Task<Response<GetTimeTableDto>> GetTimeTableById(int id);
    public Task<Response<string>> UpdateTimeTable(UpdateTimeTableDto updateTime);
    public Task<Response<bool>> DeleteTimeTable(int id);
        // public Task<Response<List<GetTimeTableDto>>> GetTimeTable();

    
}
