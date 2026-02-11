using AutoMapper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;
using UniversityActivities.Application.Common.Helpers;
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
        private readonly IMapper _mapper;
        private readonly IImageStorageService _imageStorageService;


        public UpdateActivityUseCase(
            IUnitOfWork unitOfWork,
            IImageStorageService imageStorageService,
             IMapper mapper)
        {
            _unitOfWork = unitOfWork;

            _mapper = mapper;
            _imageStorageService = imageStorageService;

        }

        public async Task<CreateOrUpdateActivityDto> GetDetailsAsync(int activityId)
        {
                var activity=await _unitOfWork.AdminActivities.GetEntityAsync(activityId);

            return  _mapper.Map<CreateOrUpdateActivityDto>(activity);
        }
        public async Task ExecuteAsync(int activityId, CreateOrUpdateActivityDto dto, IFormFile image)
        {
            try
            {
                // =========================
                //  Validation
                // =========================
              

                // =========================
                //  Load existing activity
                // =========================
                var activity = await _unitOfWork.AdminActivities
                    .GetEntityAsync(activityId);

                if (activity == null)
                    throw new InvalidOperationException("Activity not found.");


                var activityDto = _mapper.Map<Activity>(dto);
                activity.ActivityStatusId = Helpers.CalculateActivityStatus(dto.StartDate, dto.EndDate);
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
                // 4️⃣ Persist core update
                // =========================
                await _unitOfWork.AdminActivities.UpdateAsync(activityDto);

                // =========================
                // 5️⃣ Replace Target Audiences
                // =========================
                if (dto.TargetAudienceIds != null && dto.TargetAudienceIds.Count > 0) 
                {
                    await _unitOfWork.ActivityTargetAudiences
                        .ReplaceAsync(activityId, dto.TargetAudienceIds);
                }
                if (dto.Assignments != null)
                {
                    await _unitOfWork.ActivityAssignments
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
