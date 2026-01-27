using UniversityActivities.Application.DTOs.Activities;

public interface IUpdateActivityUseCase
{
    Task ExecuteAsync(int activityId, CreateOrUpdateActivityDto dto);
}
