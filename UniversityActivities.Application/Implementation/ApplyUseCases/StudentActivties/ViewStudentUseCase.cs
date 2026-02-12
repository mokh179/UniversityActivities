using System;
using System.Collections.Generic;
using System.Text;
using UniversityActivities.Application.Common.Models;
using UniversityActivities.Application.DTOs.Activities.Student;
using UniversityActivities.Application.Interfaces.IUnitOfWork;
using UniversityActivities.Application.Interfaces.Repositories.Activies.StudentActivies;
using UniversityActivities.Application.UserCases.Activities.Student;

namespace UniversityActivities.Application.Implementation.ApplyUseCases.StudentActivties
{
    public class ViewStudentUseCase : IViewStudentActivitiesUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStudentActivityQueryRepository _studentActivityQueryRepository;

        public ViewStudentUseCase(
            IUnitOfWork unitOfWork,
            IStudentActivityQueryRepository studentActivityQueryRepository)
        {
            _unitOfWork = unitOfWork;
            _studentActivityQueryRepository = studentActivityQueryRepository;
        }

        public async Task<PagedResult<StudentActivityListItemDto>> ExecuteAsync(
       int studentId,
       List<int> studentTargetAudienceIds,
       StudentActivityFilter filter,
       PagedRequest paging)
        {
            return await _studentActivityQueryRepository
                .GetPublishedActivitiesAsync(
                  studentId,
                    studentTargetAudienceIds,
                    filter,
                    paging);
        }
        public async Task<StudentActivityDetailsDto?> ExecuteAsync(
            int activityId, int studentId)
        {
            return await _studentActivityQueryRepository.GetDetailsAsync(activityId, studentId);
        }

  
    }

    }

