using System.Diagnostics;
using System.ComponentModel;
using System;
using System.Data.Common;
using AutoMapper;
using Domain.DTOs.BonusDTOFirst;
using Domain.DTOs.GroupDTO;
using Domain.Entities;
using Domain.Responses;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Domain.Filter;

namespace Infrastructure.Services.GroupService;

public class GroupService : IGroupService
{

    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public GroupService(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<Response<string>> CreateGroupAsync(AddGroupDTO group)
    {
        try
        {
            var existingGroup = await _context.Groups.FirstOrDefaultAsync(x => x.GroupName == group.GroupName);
            if (existingGroup != null)
                return new Response<string>(System.Net.HttpStatusCode.BadRequest, "Group already exists");
            var mapped = _mapper.Map<Group>(group);

            await _context.Groups.AddAsync(mapped);
            await _context.SaveChangesAsync();

            return new Response<string>("Successfully created a new Group");
        }
        catch (Exception e)
        {
            return new Response<string>(System.Net.HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<bool>> DeleteGroupAsync(int id)
    {
        try
        {
            var gr = await _context.Groups.Where(x => x.Id == id).ExecuteDeleteAsync();
            if (gr == 0)
                return new Response<bool>(System.Net.HttpStatusCode.BadRequest, "Group not found");

            return new Response<bool>(true);
        }
        catch (Exception e)
        {
            return new Response<bool>(System.Net.HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<List<GroupWithCountStudent>>> GetStudentWithCountOfStudentDtoAsync()
    {
        try
        {
            var existing = await (from g in _context.Groups
                                  let count = _context.StudentGroups.Count(x => x.GroupId == g.Id)
                                  select new GroupWithCountStudent
                                  {
                                        Group = g,
                                        Cnt = count

                                  }).ToListAsync();
            return new Response<List<GroupWithCountStudent>>(existing);
        }
        catch (Exception e)
        {
            return new Response<List<GroupWithCountStudent>>(System.Net.HttpStatusCode.InternalServerError, e.Message);
        }
    }


    public async Task<Response<GetGroupDTO>> GetGroupByIdAsync(int id)
    {
        try
        {
            var gr = await _context.Groups.FirstOrDefaultAsync(x => x.Id == id);
            if (gr == null)
                return new Response<GetGroupDTO>(System.Net.HttpStatusCode.BadRequest, "Group not found");
            var mapped = _mapper.Map<GetGroupDTO>(gr);
            return new Response<GetGroupDTO>(mapped);
        }
        catch (Exception e)
        {
            return new Response<GetGroupDTO>(System.Net.HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<PagedResponse<List<GetGroupDTO>>> GetGroupsAsync(GroupFilter filter)
    {
        try
        {
            var groups = _context.Groups.AsQueryable();
            if (string.IsNullOrEmpty(filter.GroupName))
                groups = groups.Where(e => e.GroupName.ToLower().Contains(filter.GroupName.ToLower()));

            if (filter.Status != null)
                groups = groups.Where(e => e.Status == filter.Status);
            var response = await groups
            .Skip((filter.PageNumber - 1) * filter.PageSize)
            .Take(filter.PageSize).ToListAsync();

            var totalRecord = groups.Count();

            var mapped = _mapper.Map<List<GetGroupDTO>>(response);
            return new PagedResponse<List<GetGroupDTO>>(mapped, filter.PageNumber, filter.PageSize, totalRecord);

        }
        catch (DbException dbEx)
        {
            return new PagedResponse<List<GetGroupDTO>>(System.Net.HttpStatusCode.InternalServerError, dbEx.Message);
        }
        catch (Exception ex)
        {
            return new PagedResponse<List<GetGroupDTO>>(System.Net.HttpStatusCode.InternalServerError, ex.Message);
        }
    }


    public async Task<Response<string>> UpdateGroupAsync(UpdateGroupDTO group)
    {
        try
        {
            var mappedGroup = _mapper.Map<Group>(group);
            _context.Groups.Update(mappedGroup);
            var update = await _context.SaveChangesAsync();
            if (update == 0) return new Response<string>(System.Net.HttpStatusCode.BadRequest, "Group not found");
            return new Response<string>("Group updated successfully");
        }
        catch (Exception e)
        {
            return new Response<string>(System.Net.HttpStatusCode.InternalServerError, e.Message);
        }
    }



}
