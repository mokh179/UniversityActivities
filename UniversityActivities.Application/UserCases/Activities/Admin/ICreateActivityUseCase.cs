using UniversityActivities.Application.DTOs.Activities;

public interface ICreateActivityUseCase
{
    Task<int> ExecuteAsync(CreateOrUpdateActivityDto dto);
}
