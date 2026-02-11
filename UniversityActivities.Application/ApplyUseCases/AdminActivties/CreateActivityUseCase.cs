using AutoMapper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using UniversityActivities.Application.Common.Helpers;
using UniversityActivities.Application.DTOs.Activities;
using UniversityActivities.Application.Interfaces.ImageStorage;
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
        private readonly IImageStorageService _imageStorageService;
        private readonly IMapper _mapper;
        public CreateActivityUseCase(IUnitOfWork unitOfWork,
            IAdminActivityRepository adminActivityRepository,
            IActivityTargetAudienceRepository targetAudienceRepository,
            IActivityAssignmentRepository assignmentRepository,
            IImageStorageService imageStorageService,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _adminActivityRepository = adminActivityRepository;
            _targetAudienceRepository = targetAudienceRepository;
            _assignmentRepository = assignmentRepository;
            _imageStorageService = imageStorageService;
            _mapper = mapper;
        }

        public async Task<int> ExecuteAsync(CreateOrUpdateActivityDto dto,IFormFile image)
        {
            try
            {
                // =========================
                // 2️⃣ Create Activity Entity
                // =========================

                var activity = _mapper.Map<Domain.Entities.Activity>(dto);
                activity.ActivityStatusId = Helpers.CalculateActivityStatus(dto.StartDate, dto.EndDate);

                // =========================
                // Image Saving
                // =========================
                var ImageUrl = await _imageStorageService.SaveOrReplaceActivityImageAsync(image, Guid.NewGuid(), activity.TitleEn, null);
                activity.ImageUrl = ImageUrl;
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
            catch (Exception ex)
            {

                throw ex;
            }

            
        }
    }
}
