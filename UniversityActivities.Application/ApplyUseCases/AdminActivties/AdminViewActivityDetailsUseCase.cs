
using System;
using System.Collections.Generic;
using System.Text;
using UniversityActivities.Application.DTOs.Activities;
using UniversityActivities.Application.Interfaces.Repositories.Activies.AdminActivies;
using UniversityActivities.Application.UserCases.Activities.Admin;

namespace UniversityActivities.Application.ApplyUseCases.AdminActivties
{
    public class AdminViewActivityDetailsUseCase:IAdminViewActivityDetailsUseCase     
    {
        private readonly IAdminActivityRepository _adminActivityRepository;

        public AdminViewActivityDetailsUseCase(
            IAdminActivityRepository adminActivityRepository)
        {
            _adminActivityRepository = adminActivityRepository;
        }

        public async Task<ActivityAdminDetailsDto?> ExecuteAsync(int activityId)
        {
           return await _adminActivityRepository.GetDetailsAsync(activityId);

        }
    }
}
