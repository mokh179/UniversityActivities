using System;
using System.Collections.Generic;
using System.Text;
using UniversityActivities.Application.Interfaces.IUnitOfWork;
using UniversityActivities.Application.Interfaces.Repositories.Activies.AdminActivies;
using UniversityActivities.Application.UserCases.Activities.Admin;

namespace UniversityActivities.Application.Implementation.ApplyUseCases.AdminActivties
{
    public class DeleteActivityUseCase:IDeleteActivityUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        //private readonly IAdminActivityRepository _adminActivityRepository;

        public DeleteActivityUseCase(
            IUnitOfWork unitOfWork
           )
        {
            _unitOfWork = unitOfWork;
            //_adminActivityRepository = adminActivityRepository;
        }
        public async Task ExecuteAsync(int activityId)
        {
            // =========================
            //  Load entity
            // =========================
             await _unitOfWork.AdminActivities
                .DeleteAsync(activityId);


            // =========================
            //  Commit
            // =========================
            await _unitOfWork.SaveChangesAsync();
        }

    }
}
