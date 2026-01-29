using System;
using System.Collections.Generic;
using System.Text;
using UniversityActivities.Application.Interfaces.IUnitOfWork;
using UniversityActivities.Application.Interfaces.Repositories.Activies.AdminActivies;
using UniversityActivities.Application.UserCases.Activities.Admin;

namespace UniversityActivities.Application.ApplyUseCases.AdminActivties
{
    public class DeleteActivityUseCase:IDeleteActivityUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAdminActivityRepository _adminActivityRepository;

        public DeleteActivityUseCase(
            IUnitOfWork unitOfWork,
            IAdminActivityRepository adminActivityRepository)
        {
            _unitOfWork = unitOfWork;
            _adminActivityRepository = adminActivityRepository;
        }
        public async Task ExecuteAsync(int activityId)
        {
            // =========================
            //  Load entity
            // =========================
            var activity = await _adminActivityRepository
                .GetEntityAsync(activityId);

            if (activity == null)
                throw new InvalidOperationException("Activity not found.");

            // =========================
            //  Soft delete
            // =========================
            activity.IsDeleted = true;

            // =========================
            //  Commit
            // =========================
            await _unitOfWork.SaveChangesAsync();
        }

    }
}
