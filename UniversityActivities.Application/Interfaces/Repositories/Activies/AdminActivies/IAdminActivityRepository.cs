using System;
using System.Collections.Generic;
using System.Text;
using UniversityActivities.Application.Common.Models;
using UniversityActivities.Application.DTOs.Activities;
using UniversityActivities.Domain.Entities;

namespace UniversityActivities.Application.Interfaces.Repositories.Activies.AdminActivies
{
    public interface IAdminActivityRepository
    {
        Task<int> CreateAsync(Activity activity);

        Task UpdateAsync(Activity activity);

        Task DeleteAsync(int activityId);

        Task SetPublishStatusAsync(int activityId, bool isPublished);

        Task<PagedResult<ActivityAdminListItemDto>> GetAllAsync(
            ActivityAdminFilter filter,
            PagedRequest paging);

        Task<ActivityAdminDetailsDto?> GetDetailsAsync(int activityId);
        Task<Activity?> GetEntityAsync(int activityId);
    }
}
