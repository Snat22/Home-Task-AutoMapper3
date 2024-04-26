using System.Data.Common;
using System.Net;
using AutoMapper;
using Domain.DTOs.TimeTableDtos;
using Domain.Entities;
using Domain.Filter;
using Domain.Responses;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.TimeTableServices;

public class TimeTableService(DataContext context, IMapper mapper) : ITimeTableSrvice
{
    public async Task<Response<string>> AddTimeTable(AddTimeTableDto addTime)
    {
        try
        {

            var mapped = mapper.Map<TimeTable>(addTime);
            await context.TimeTables.AddAsync(mapped);
            await context.SaveChangesAsync();
            return new Response<string>(System.Net.HttpStatusCode.Accepted, "Success");
        }
        catch (System.Exception e)
        {

        
        return new Response<string>(System.Net.HttpStatusCode.InternalServerError,e.Message);
        }
        
    }

    public async Task<PagedResponse<List<GetTimeTableDto>>> GetTimeTableFillter(TimeTableFilter filter)
    {
        try
        {
            var time = context.TimeTables.AsQueryable();
            var response = await time
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize).ToListAsync();
            var totalRecord = time.Count();

            var mapped = mapper.Map<List<GetTimeTableDto>>(response);
            return new PagedResponse<List<GetTimeTableDto>>(mapped, filter.PageNumber, filter.PageSize, totalRecord);

        }
        catch (DbException dbEx)
        {
            return new PagedResponse<List<GetTimeTableDto>>(System.Net.HttpStatusCode.InternalServerError, dbEx.Message);
        }
        catch (Exception ex)
        {
            return new PagedResponse<List<GetTimeTableDto>>(System.Net.HttpStatusCode.InternalServerError, ex.Message);
        }
    
    }


    public async Task<Response<GetTimeTableDto>> GetTimeTableById(int id)
    {
        try
        {
            var time = await context.TimeTables.FirstOrDefaultAsync(x => x.Id == id);
            if (time == null)
                return new Response<GetTimeTableDto>(System.Net.HttpStatusCode.BadRequest, "TimeTable not found");
            var mapped = mapper.Map<GetTimeTableDto>(time);
            return new Response<GetTimeTableDto>(mapped);
        }
        catch (Exception e)
        {
            return new Response<GetTimeTableDto>(System.Net.HttpStatusCode.InternalServerError, e.Message);
        }
    }

    
    public async Task<Response<string>> UpdateTimeTable(UpdateTimeTableDto timeTable)
    {
        try
        {
            var mappedtimeTable = mapper.Map<TimeTable>(timeTable);
            context.TimeTables.Update(mappedtimeTable);
            var update = await context.SaveChangesAsync();
            if (update == 0) return new Response<string>(System.Net.HttpStatusCode.BadRequest, "timeTable not found");
            return new Response<string>("timeTable updated successfully");
        }
        catch (Exception e)
        {
            return new Response<string>(System.Net.HttpStatusCode.InternalServerError, e.Message);
        }
    }

public Task<Response<bool>> DeleteTimeTable(int id)
    {
        throw new NotImplementedException();
    }

}
