using System;
using System.Collections.Generic;
using System.Text;
using UniversityActivities.Application.DTOs.Activities;
using UniversityActivities.Application.Interfaces.IUnitOfWork;
using UniversityActivities.Application.Interfaces.Repositories.Activies.AdminActivies;

namespace UniversityActivities.Application.ApplyUseCases.AdminActivties
{
    public class UpdateActivityUseCase: IUpdateActivityUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAdminActivityRepository _adminActivityRepository;
        private readonly IActivityTargetAudienceRepository _targetAudienceRepository;
        private readonly IActivityAssignmentRepository _assignmentRepository;

        public UpdateActivityUseCase(
            IUnitOfWork unitOfWork,
            IAdminActivityRepository adminActivityRepository,
            IActivityTargetAudienceRepository targetAudienceRepository,
            IActivityAssignmentRepository assignmentRepository)
        {
            _unitOfWork = unitOfWork;
            _adminActivityRepository = adminActivityRepository;
            _targetAudienceRepository = targetAudienceRepository;
            _assignmentRepository = assignmentRepository;
        }

        public async Task ExecuteAsync(int activityId, CreateOrUpdateActivityDto dto)
        {
            // =========================
            //  Validation
            // =========================
            if (dto.StartDate >= dto.EndDate)
                throw new InvalidOperationException(
                    "Activity start date must be before end date.");

            // =========================
            //  Load existing activity
            // =========================
            var activity = await _adminActivityRepository
                .GetEntityAsync(activityId);

            if (activity == null)
                throw new InvalidOperationException("Activity not found.");
            // =========================
            // 3️ Update core fields
            // =========================
            activity.TitleAr = dto.TitleAr;
            activity.TitleEn = dto.TitleEn;
            activity.DescriptionAr = dto.DescriptionAr;
            activity.DescriptionEn = dto.DescriptionEn;

            activity.ImageUrl = dto.ImageUrl;

            activity.StartDateTime = dto.StartDate;
            activity.EndDateTime = dto.EndDate;

            activity.LocationAr = dto.LocationAr;
            activity.LocationEn = dto.LocationEn;
            activity.OnlineLink = dto.OnlineLink;

            activity.ManagementId = dto.ManagementId;
            activity.ActivityTypeId = dto.ActivityTypeId;
            activity.AttendanceModeId = dto.AttendanceModeId;

            activity.IsPublished = dto.IsPublished;

            // =========================
            // 4️⃣ Persist core update
            // =========================
            await _adminActivityRepository.UpdateAsync(activity);

            // =========================
            // 5️⃣ Replace Target Audiences
            // =========================
            if (dto.TargetAudienceIds != null)
            {
                await _targetAudienceRepository
                    .ReplaceAsync(activityId, dto.TargetAudienceIds);
            }

            throw new NotImplementedException();
        }
    }
}
