using Microsoft.AspNetCore.Http;
using UniversityActivities.Application.DTOs.Activities;

public interface ICreateActivityUseCase
{
    Task<int> ExecuteAsync(CreateOrUpdateActivityDto dto,IFormFile image);
}
