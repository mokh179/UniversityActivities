using System;
using System.Collections.Generic;
using System.Text;
using UniversityActivities.Application.Common.Models;
using UniversityActivities.Application.DTOs.Activities.Student;

namespace UniversityActivities.Application.UserCases.Activities.Student
{
    public interface IViewStudentActivitiesUseCase
    {
        Task<PagedResult<StudentActivityListItemDto>> ExecuteAsync(
            int studentId,
            List<int> studentTargetAudienceIds,
            StudentActivityFilter filter,
            PagedRequest paging);
        Task<StudentActivityDetailsDto?> ExecuteAsync(
            int activityId,int studentId);
    }
}
