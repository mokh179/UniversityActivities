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

          // Basic
          .ForMember(d => d.TitleAr, o => o.MapFrom(s => s.TitleAr))
          .ForMember(d => d.TitleEn, o => o.MapFrom(s => s.TitleEn))
          .ForMember(d => d.DescriptionAr, o => o.MapFrom(s => s.DescriptionAr ?? string.Empty))
          .ForMember(d => d.DescriptionEn, o => o.MapFrom(s => s.DescriptionEn))
          .ForMember(d => d.ImageUrl, o => o.MapFrom(s => s.ImageUrl))
          .ForMember(d => d.ActivityCode, o => o.MapFrom(s => s.ActivityCode))

          // Ownership
          .ForMember(d => d.ManagementId, o => o.MapFrom(s => s.ManagementId))
          .ForMember(d => d.StudentClubId, o => o.MapFrom(s => s.ClubId))

          // Lookups
          .ForMember(d => d.ActivityTypeId, o => o.MapFrom(s => s.ActivityTypeId))
          .ForMember(d => d.AttendanceModeId, o => o.MapFrom(s => s.AttendanceModeId))
          .ForMember(d => d.AttendanceScopeId, o => o.MapFrom(s => s.AttendanceScopeId))

          // Time
          .ForMember(d => d.StartDateTime, o => o.MapFrom(s => s.StartDate))
          .ForMember(d => d.EndDateTime, o => o.MapFrom(s => s.EndDate))

          .ForMember(d => d.IsPublished, o => o.MapFrom(s => s.IsPublished))

          // Target Audiences
          .ForMember(d => d.ActivityTargetAudiences,
              o => o.MapFrom(s =>
                  s.TargetAudienceIds.Select(id =>
                      new ActivityTargetAudience
                      {
                          TargetAudienceId = id
                      })))

          // Ignore Navigations
          .ForMember(d => d.Management, o => o.Ignore())
          .ForMember(d => d.StudentClub, o => o.Ignore())
          .ForMember(d => d.ActivityStatus, o => o.Ignore())
          .ForMember(d => d.ActivityType, o => o.Ignore())
          .ForMember(d => d.AttendanceScope, o => o.Ignore())
          .ForMember(d => d.AttendanceMode, o => o.Ignore())
          .ForMember(d => d.ActivityUsers, o => o.Ignore())
          .ForMember(d => d.StudentActivities, o => o.Ignore())
          .ForMember(d => d.Evaluations, o => o.Ignore());



        // ==============================
        // Entity ➜ DTO
        // ==============================
        CreateMap<Activity, CreateOrUpdateActivityDto>()

            // Basic
            .ForMember(d => d.TitleAr, o => o.MapFrom(s => s.TitleAr))
            .ForMember(d => d.TitleEn, o => o.MapFrom(s => s.TitleEn))
            .ForMember(d => d.DescriptionAr, o => o.MapFrom(s => s.DescriptionAr))
            .ForMember(d => d.DescriptionEn, o => o.MapFrom(s => s.DescriptionEn))
            .ForMember(d => d.ImageUrl, o => o.MapFrom(s => s.ImageUrl))
            .ForMember(d => d.ActivityCode, o => o.MapFrom(s => s.ActivityCode))

            // Ownership
            .ForMember(d => d.ManagementId, o => o.MapFrom(s => s.ManagementId))
            .ForMember(d => d.ClubId, o => o.MapFrom(s => s.StudentClubId))

            // Lookups
            .ForMember(d => d.ActivityTypeId, o => o.MapFrom(s => s.ActivityTypeId))
            .ForMember(d => d.AttendanceModeId, o => o.MapFrom(s => s.AttendanceModeId))
            .ForMember(d => d.AttendanceScopeId, o => o.MapFrom(s => s.AttendanceScopeId))

            // Time
            .ForMember(d => d.StartDate, o => o.MapFrom(s => s.StartDateTime))
            .ForMember(d => d.EndDate, o => o.MapFrom(s => s.EndDateTime))

            .ForMember(d => d.IsPublished, o => o.MapFrom(s => s.IsPublished))

            // Target Audiences reverse
            .ForMember(d => d.TargetAudienceIds,
                o => o.MapFrom(s =>
                    s.ActivityTargetAudiences
                     .Select(x => x.TargetAudienceId)
                     .ToList()))

            // Assignments reverse
            .ForMember(d => d.Assignments,
                o => o.MapFrom(s =>
                    s.ActivityUsers
                     .Select(au => new ActivityAssignmentDto
                     {
                         UserId = au.UserId,
                         ActivityRoleId = au.ActivityRoleId
                     })
                     .ToList()))

            // Ignore DTO-only fields
            .ForMember(d => d.ManagementTypeId, o => o.Ignore());

    }
}

