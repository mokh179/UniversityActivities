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
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.IsPublished, opt => opt.Ignore())
            .ForMember(dest => dest.ActivityTargetAudiences, opt => opt.Ignore());
    }
}
