using System;
using System.Collections.Generic;
using System.Text;
using UniversityActivities.Application.DTOs.Activities;
using UniversityActivities.Application.Interfaces.IUnitOfWork;
using UniversityActivities.Application.Interfaces.Repositories.Activies.AdminActivies;

namespace UniversityActivities.Application.ApplyUseCases.AdminActivties
{
    public class CreateActivityUseCase : ICreateActivityUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAdminActivityRepository _adminActivityRepository;
        private readonly IActivityTargetAudienceRepository _targetAudienceRepository;
        private readonly IActivityAssignmentRepository _assignmentRepository;
        public CreateActivityUseCase(IUnitOfWork unitOfWork,
            IAdminActivityRepository adminActivityRepository,
            IActivityTargetAudienceRepository targetAudienceRepository,
            IActivityAssignmentRepository assignmentRepository)
        {
            _unitOfWork = unitOfWork;
            _adminActivityRepository = adminActivityRepository;
            _targetAudienceRepository = targetAudienceRepository;
            _assignmentRepository = assignmentRepository;
        }

        public async Task<int> ExecuteAsync(CreateOrUpdateActivityDto dto)
        {
            int status = 0;
            // =========================
            // 1️⃣ Validation (basic)
            // =========================
            if (dto.StartDate >= dto.EndDate)
                throw new InvalidOperationException("Activity start date must be before end date.");
            if (dto.StartDate == DateTime.Now)
                status = 1;
            if (dto.StartDate > DateTime.Now)
                status = 2;

            // =========================
            // 2️⃣ Create Activity Entity
            // =========================
            var activity = new Domain.Entities.Activity
            {
                TitleAr = dto.TitleAr,
                TitleEn = dto.TitleEn,
                DescriptionAr = dto.DescriptionAr,
                DescriptionEn = dto.DescriptionEn,

                ImageUrl = dto.ImageUrl,

                StartDateTime = dto.StartDate,
                EndDateTime = dto.EndDate,

                LocationAr = dto.LocationAr,
                LocationEn = dto.LocationEn,
                OnlineLink = dto.OnlineLink,
                ActivityStatusId=status,
                ManagementId = dto.ManagementId,
                ActivityTypeId = dto.ActivityTypeId,
                AttendanceModeId = dto.AttendanceModeId,
                IsPublished = dto.IsPublished
            };

            // =========================
            //  Persist Activity
            // =========================
            var activityId = await _adminActivityRepository
                .CreateAsync(activity);

            // =========================
            //  Assign Target Audiences
            // =========================
            if (dto.TargetAudienceIds != null && dto.TargetAudienceIds.Count > 0)
            {
                await _targetAudienceRepository
                    .ReplaceAsync(activityId, dto.TargetAudienceIds);
            }

            // =========================
            //  Assign Roles (Supervisors / Coordinators / Viewers)
            // =========================
            if (dto.Assignments != null && dto.Assignments.Count > 0)
            {
                await _assignmentRepository
                    .ReplaceAsync(activityId, dto.Assignments);
            }

            // =========================
            //  Commit
            // =========================
            await _unitOfWork.SaveChangesAsync();

            return activityId;
        }
    }
}
