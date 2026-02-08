using UniversityActivities.Application.DTOs.Activities;

public interface IUpdateActivityUseCase
{
    Task<CreateOrUpdateActivityDto> GetDetailsAsync(int activityId);
    Task ExecuteAsync(int activityId, CreateOrUpdateActivityDto dto);
}
