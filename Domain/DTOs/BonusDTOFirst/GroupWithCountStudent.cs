using Domain.DTOs.GroupDTO;
using Domain.Entities;

namespace Domain.DTOs.BonusDTOFirst;

public class GroupWithCountStudent
{
    public Group? Group { get; set; }
    public int Cnt { get; set; }
}
