using System.Data.Common;
using AutoMapper;
using Domain.DTOs.CourseDTO;
using Domain.Entities;
using Domain.Filter;
using Domain.Responses;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.CourseService;

public class CourseService : ICourseService
{
    
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public CourseService(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<Response<string>> CreateCourseAsync(AddCourseDTO course)
    {
       try
        {
            var existingCourse = await _context.Courses.FirstOrDefaultAsync(x => x.Description == course.Description);
            if (existingCourse != null)
                return new Response<string>(System.Net.HttpStatusCode.BadRequest, "Student already exists");
            var mapped = _mapper.Map<Course>(course);

            await _context.Courses.AddAsync(mapped);
            await _context.SaveChangesAsync();

            return new Response<string>("Successfully created a new Course");
        }
        catch (Exception e)
        {
            return new Response<string>(System.Net.HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<bool>> DeleteCourseAsync(int id)
    {
        try
        {
            var cr = await _context.Courses.Where(x => x.Id == id).ExecuteDeleteAsync();
            if (cr == 0)
                return new Response<bool>(System.Net.HttpStatusCode.BadRequest, "Course not found");

            return new Response<bool>(true);
        }
        catch (Exception e)
        {
            return new Response<bool>(System.Net.HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<GetCourseDTO>> GetCourseByIdAsync(int id)
    {
          try
        {
            var cr = await _context.Courses.FirstOrDefaultAsync(x => x.Id == id);
            if (cr == null)
                return new Response<GetCourseDTO>(System.Net.HttpStatusCode.BadRequest, "Course not found");
            var mapped = _mapper.Map<GetCourseDTO>(cr);
            return new Response<GetCourseDTO>(mapped);
        }
        catch (Exception e)
        {
            return new Response<GetCourseDTO>(System.Net.HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<PagedResponse<List<GetCourseDTO>>> GetCoursesAsync(CourseFilter filter)
    {
        var course = _context.Courses.AsQueryable();
         try
        {
            if (!string.IsNullOrEmpty(filter.CourseName))
            course = course.Where(e => e.CourseName.ToLower().Contains(filter.CourseName));
            if (filter.Status!=null)
            course = course.Where(e => e.Status == filter.Status);
            var response = await course
            .Skip((filter.PageNumber -1) * filter.PageSize)
            .Take(filter.PageSize).ToListAsync();
            var totalRecord = course.Count();
            var mapped = _mapper.Map<List<GetCourseDTO>>(response);
            return new PagedResponse<List<GetCourseDTO>>(mapped,filter.PageNumber,filter.PageSize,totalRecord);
        }
        catch (DbException dbEx)
        {
            return new PagedResponse<List<GetCourseDTO>>(System.Net.HttpStatusCode.InternalServerError, dbEx.Message);
        }
        catch (Exception ex)
        {
            return new PagedResponse<List<GetCourseDTO>>(System.Net.HttpStatusCode.InternalServerError, ex.Message);
        }
    }

    public async Task<Response<string>> UpdateCourseAsync(UpdateCourseDTO course)
    {
        try
        {
            var mappedCourse = _mapper.Map<Course>(course);
            _context.Courses.Update(mappedCourse);
            var update= await _context.SaveChangesAsync();
            if(update==0)  return new Response<string>(System.Net.HttpStatusCode.BadRequest, "Course not found");
            return new Response<string>("Course updated successfully");
        }
        catch (Exception e)
        {
            return new Response<string>(System.Net.HttpStatusCode.InternalServerError, e.Message);
        }
    }

}
