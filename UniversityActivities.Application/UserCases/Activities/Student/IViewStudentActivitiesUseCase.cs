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
            int studentManagementId,
            List<int> studentTargetAudienceIds,
            StudentActivityFilter filter,
            PagedRequest paging);
    }
}
