using System;
using System.Collections.Generic;
using System.Text;
using UniversityActivities.Application.Common.Models;
using UniversityActivities.Application.DTOs.Activities;
using UniversityActivities.Application.Interfaces.Repositories.Activies.AdminActivies;
using UniversityActivities.Application.UserCases.Activities.Admin;

namespace UniversityActivities.Application.ApplyUseCases.AdminActivties
{
    public class ViewAdminActivitiesUseCase: IViewAdminActivitiesUseCase
    {
        private readonly IAdminActivityRepository _adminActivityRepository;

        public ViewAdminActivitiesUseCase(
            IAdminActivityRepository adminActivityRepository)
        {
            _adminActivityRepository = adminActivityRepository;
        }

        public async Task<PagedResult<ActivityAdminListItemDto>> ExecuteAsync(
            ActivityAdminFilter filter,
            PagedRequest paging)
        {
            return await _adminActivityRepository
                .GetAllAsync(filter, paging);
        }
    }
}
