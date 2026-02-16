
using System;
using System.Collections.Generic;
using System.Text;
using UniversityActivities.Application.DTOs.Activities;
using UniversityActivities.Application.Interfaces.IUnitOfWork;
using UniversityActivities.Application.Interfaces.Repositories.Activies.AdminActivies;
using UniversityActivities.Application.UserCases.Activities.Admin;

namespace UniversityActivities.Application.Implementation.ApplyUseCases.AdminActivties
{
    public class AdminViewActivityDetailsUseCase:IAdminViewActivityDetailsUseCase     
    {
        //private readonly IAdminActivityRepository _adminActivityRepository;
        private readonly IUnitOfWork _unitofwork;

        public AdminViewActivityDetailsUseCase(
            IUnitOfWork unitofwork)
        {
            //_adminActivityRepository = adminActivityRepository;
            _unitofwork = unitofwork;
        }

        public async Task<ActivityAdminDetailsDto?> ExecuteAsync(int activityId)
        {
           return await _unitofwork.AdminActivities.GetDetailsAsync(activityId);
        }
    }
}
