using System;
using System.Collections.Generic;
using System.Text;
using UniversityActivities.Application.Common.Models;
using UniversityActivities.Application.DTOs.Activities.Student;

namespace UniversityActivities.Application.Interfaces.Repositories.Activies.StudentActivies
{
    public interface IStudentActivityQueryRepository
    {
        Task<PagedResult<StudentActivityListItemDto>> GetPublishedActivitiesAsync(int studentid,
       List<int> studentTargetAudienceIds,
       StudentActivityFilter filter,
       PagedRequest paging);

        Task<StudentActivityDetailsDto?> GetDetailsAsync(
            int activityId,
            int studentId);

        Task<StudentActivityCertificateDto?> GetCertificateDetails(int activityId, int studentId);
    }
}
