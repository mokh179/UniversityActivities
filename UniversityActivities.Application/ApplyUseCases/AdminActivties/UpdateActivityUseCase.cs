using AutoMapper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;
using UniversityActivities.Application.DTOs.Activities;
using UniversityActivities.Application.Exceptions;
using UniversityActivities.Application.Interfaces.ImageStorage;
using UniversityActivities.Application.Interfaces.IUnitOfWork;
using UniversityActivities.Application.Interfaces.Repositories.Activies.AdminActivies;
using UniversityActivities.Domain.Entities;
using static System.Net.Mime.MediaTypeNames;

namespace UniversityActivities.Application.ApplyUseCases.AdminActivties
{
    public class UpdateActivityUseCase: IUpdateActivityUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAdminActivityRepository _adminActivityRepository;
        private readonly IActivityTargetAudienceRepository _targetAudienceRepository;
        private readonly IActivityAssignmentRepository _assignmentRepository;
        private readonly IMapper _mapper;
        private readonly IImageStorageService _imageStorageService;


        public UpdateActivityUseCase(
            IUnitOfWork unitOfWork,
            IAdminActivityRepository adminActivityRepository,
            IActivityTargetAudienceRepository targetAudienceRepository,
            IImageStorageService imageStorageService,
            IActivityAssignmentRepository assignmentRepository, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _adminActivityRepository = adminActivityRepository;
            _targetAudienceRepository = targetAudienceRepository;
            _assignmentRepository = assignmentRepository;
            _mapper = mapper;
            _imageStorageService = imageStorageService;

        }

        public async Task<CreateOrUpdateActivityDto> GetDetailsAsync(int activityId)
        {
                var activity=await _adminActivityRepository.GetEntityAsync(activityId);

            return  _mapper.Map<CreateOrUpdateActivityDto>(activity);
        }
        public async Task ExecuteAsync(int activityId, CreateOrUpdateActivityDto dto, IFormFile image)
        {
            try
            {
                // =========================
                //  Validation
                // =========================
                int status = 0;

                if (dto.StartDate >= dto.EndDate)
                    throw new InvalidOperationException("Activity start date must be before end date.");
                if (dto.StartDate.Day == DateTime.Now.Day)
                    status = 1;
                if (dto.StartDate > DateTime.Now)
                    status = 3;

                // =========================
                //  Load existing activity
                // =========================
                var activity = await _adminActivityRepository
                    .GetEntityAsync(activityId);

                if (activity == null)
                    throw new InvalidOperationException("Activity not found.");


                var activityDto = _mapper.Map<Activity>(dto);
                activityDto.ActivityStatusId = activity.ActivityStatusId;
                activityDto.ImageUrl = activity.ImageUrl;
                if (activityDto.ActivityTargetAudiences.Count > 0)
                    activityDto.ActivityTargetAudiences = activity.ActivityTargetAudiences;
                // =========================
                // Image Saving
                // =========================
                if (image != null)
                {
                    var ImageUrl = await _imageStorageService.SaveOrReplaceActivityImageAsync(image, Guid.NewGuid(), activity.TitleEn, activity.ImageUrl);
                    activityDto.ImageUrl = ImageUrl;
                }
                // =========================
                // 3️ Update core fields
                // =========================
                //activity.TitleAr = dto.TitleAr;
                //activity.TitleEn = dto.TitleEn;
                //activity.DescriptionAr = dto.DescriptionAr;
                //activity.DescriptionEn = dto.DescriptionEn;

                //activity.ImageUrl = ImageUrl;
                //activity.StartDateTime = dto.StartDate;
                //activity.EndDateTime = dto.EndDate;

                //activity.LocationAr = dto.LocationAr;
                //activity.LocationEn = dto.LocationEn;
                //activity.OnlineLink = dto.OnlineLink;

                //activity.ManagementId = dto.ManagementId;
                //activity.ActivityTypeId = dto.ActivityTypeId;
                //activity.AttendanceModeId = dto.AttendanceModeId;

                //activity.IsPublished = dto.IsPublished;
                // =========================
                // 4️⃣ Persist core update
                // =========================
                await _adminActivityRepository.UpdateAsync(activityDto);

                // =========================
                // 5️⃣ Replace Target Audiences
                // =========================
                if (dto.TargetAudienceIds != null && dto.TargetAudienceIds.Count > 0) 
                {
                    await _targetAudienceRepository
                        .ReplaceAsync(activityId, dto.TargetAudienceIds);
                }
                if (dto.Assignments != null)
                {
                    await _assignmentRepository
                        .ReplaceAsync(activityId, dto.Assignments);
                }

                // =========================
                //  Commit
                // =========================
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw new BusinessException("Error occured while updating . Try Again in a while ...");
            }
        }
    }
}
