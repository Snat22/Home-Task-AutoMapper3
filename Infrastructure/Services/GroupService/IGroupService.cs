using Domain.DTOs.BonusDTOFirst;
using Domain.DTOs.GroupDTO;
using Domain.Filter;
using Domain.Responses;

namespace Infrastructure.Services.GroupService;

public interface IGroupService
{
    
    Task<PagedResponse<List<GetGroupDTO>>> GetGroupsAsync(GroupFilter filter);
    Task<Response<GetGroupDTO>> GetGroupByIdAsync(int id);
    Task<Response<string>> CreateGroupAsync(AddGroupDTO group);
    Task<Response<string>> UpdateGroupAsync(UpdateGroupDTO group);
    Task<Response<bool>> DeleteGroupAsync(int id);
      Task<Response<List<GroupWithCountStudent>>> GetStudentWithCountOfStudentDtoAsync();
}
