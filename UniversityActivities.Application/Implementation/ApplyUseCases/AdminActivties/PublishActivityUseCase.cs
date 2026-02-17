using System;
using System.Collections.Generic;
using System.Text;
using UniversityActivities.Application.Interfaces.IUnitOfWork;
using UniversityActivities.Application.Interfaces.Repositories.Activies.AdminActivies;
using UniversityActivities.Application.UserCases.Activities.Admin;

namespace UniversityActivities.Application.Implementation.ApplyUseCases.AdminActivties
{
    public class PublishActivityUseCase:IPublishActivityUseCase
    {

        private readonly IUnitOfWork _unitOfWork;
        //private readonly IAdminActivityRepository _adminActivityRepository;

        public PublishActivityUseCase(
            IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
           // _adminActivityRepository = adminActivityRepository;
        }

        public async Task ExecuteAsync(int activityId, bool isPublished)
        {
            // =========================
            //  Load entity
            // =========================
            var activity = await _unitOfWork.AdminActivities
                .GetEntityAsync(activityId);

            if (activity == null)
                throw new InvalidOperationException("Activity not found.");

            // =========================
            //  Set publish state
            // =========================
            activity.IsPublished = isPublished;

            // =========================
            // Commit
            // =========================
            await _unitOfWork.SaveChangesAsync();
        }
    }

}
