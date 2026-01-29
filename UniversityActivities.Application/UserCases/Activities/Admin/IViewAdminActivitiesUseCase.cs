using System;
using System.Collections.Generic;
using System.Text;
using UniversityActivities.Application.Common.Models;
using UniversityActivities.Application.DTOs.Activities;

namespace UniversityActivities.Application.UserCases.Activities.Admin
{
    public interface IViewAdminActivitiesUseCase
    {
        Task<PagedResult<ActivityAdminListItemDto>> ExecuteAsync(
        ActivityAdminFilter filter,
        PagedRequest paging);
    }
}
