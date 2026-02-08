using AutoMapper;
using UniversityActivities.Domain.Entities;
using UniversityActivities.Application.DTOs.Activities;

namespace UniversityActivities.Application.Mapping.Activities;

public class ActivityProfile : Profile
{
    public ActivityProfile()
    {
        MapCreateOrUpdate();
    }

    /// <summary>
    /// Mapping used only for Create / Update Activity
    /// All view/list projections are handled via JOIN queries in repositories
    /// </summary>
    private void MapCreateOrUpdate()
    {
        CreateMap<CreateOrUpdateActivityDto, Activity>()
    // DTO ➜ Entity
    .ForMember(dest => dest.StudentClubId,
        opt => opt.MapFrom(src => src.ClubId))

    .ForMember(dest => dest.StartDateTime,
        opt => opt.MapFrom(src => src.StartDate))

    .ForMember(dest => dest.EndDateTime,
        opt => opt.MapFrom(src => src.EndDate))

    .ForMember(dest => dest.ActivityTargetAudiences,
        opt => opt.MapFrom(src =>
            src.TargetAudienceIds.Select(id =>
                new ActivityTargetAudience
                {
                    TargetAudienceId = id
                })))

    .ForMember(dest => dest.ActivityUsers,
        opt => opt.MapFrom(src =>
            src.Assignments.Select(a =>
                new ActivityUser
                {
                    UserId = a.UserId,
                    ActivityRoleId = a.ActivityRoleId
                })))

    .ForMember(dest => dest.Management.ManagementTypeId,opt => opt.MapFrom(src =>src.ManagementTypeId))
    
    .ForMember(dest => dest.ActivityStatusId, opt => opt.Ignore())
    .ForMember(dest => dest.ActivityType, opt => opt.Ignore())
    .ForMember(dest => dest.AttendanceMode, opt => opt.Ignore())
    .ForMember(dest => dest.AttendanceScope, opt => opt.Ignore())

    // 🔁 Entity ➜ DTO
    .ReverseMap()

    .ForMember(dest => dest.ClubId,
        opt => opt.MapFrom(src => src.StudentClubId))

    .ForMember(dest => dest.StartDate,
        opt => opt.MapFrom(src => src.StartDateTime))

    .ForMember(dest => dest.EndDate,
        opt => opt.MapFrom(src => src.EndDateTime))

    .ForMember(dest => dest.TargetAudienceIds,
        opt => opt.MapFrom(src =>
            src.ActivityTargetAudiences
               .Select(x => x.TargetAudienceId)))

    .ForMember(dest => dest.Assignments,
        opt => opt.MapFrom(src =>
            src.ActivityUsers.Select(u => new ActivityAssignmentDto
            {
                UserId = u.UserId,
                ActivityRoleId = u.ActivityRoleId
            })));

    }
}

