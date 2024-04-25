using Domain.DTOs.CourseDTO;
using Domain.Filter;
using Domain.Responses;

namespace Infrastructure.Services.CourseService;

public interface ICourseService
{
    
    Task<PagedResponse<List<GetCourseDTO>>> GetCoursesAsync(CourseFilter filter);
    Task<Response<GetCourseDTO>> GetCourseByIdAsync(int id);
    Task<Response<string>> CreateCourseAsync(AddCourseDTO course);
    Task<Response<string>> UpdateCourseAsync(UpdateCourseDTO course);
    Task<Response<bool>> DeleteCourseAsync(int id);
}
