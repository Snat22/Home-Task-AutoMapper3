using Domain.DTOs.MentorDTO;
using Domain.Entities;
using Domain.Filter;
using Domain.Responses;
using Infrastructure.Services.MentorService;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("[controller]")]
[ApiController]
public class MentorController : ControllerBase
{
    private readonly IMentorService _mentorService;

    public MentorController(IMentorService mentorService)
    {
        _mentorService = mentorService;
    }

    [HttpGet("get-Mentor")]
    public async Task<Response<List<GetMentorDTO>>> GetMentorAsynccc([FromQuery] MentorFilter filter)
    {
        return await _mentorService.GetMentorsAsync(filter);
    }
    [HttpGet("{mentorId:int}")]
    public async Task<Response<GetMentorDTO>> GetMentorByIdAsync(int mentorId)
    {
        return await _mentorService.GetMentorByIdAsync(mentorId);
    }
    
    [HttpPost("create-Mentor")]
    public async Task<Response<string>> AddMentorAsync(AddMentorDTO mentorDTO)
    {
        return await _mentorService.CreateMentorAsync(mentorDTO);
    }
    
    [HttpPut("update-Mentor")]
    public async Task<Response<string>> UpdateMentorAsync(UpdateMentorDTO mentorDTO)
    {
        return await _mentorService.UpdateMentorAsync(mentorDTO);
    }
    
    [HttpDelete("{mentorId:int}")]
    public async Task<Response<bool>> DeleteStudentAsync(int mentorId)
    {
        return await _mentorService.DeleteMentorAsync(mentorId);
    }
    [HttpGet("Get-Mentor-That-Have-Group")]
    public async Task<Response<List<MentorWithGroup>>> GetMentorsThatHas()
    {
            return await _mentorService.GetMentorsThatHas();
    }

}
