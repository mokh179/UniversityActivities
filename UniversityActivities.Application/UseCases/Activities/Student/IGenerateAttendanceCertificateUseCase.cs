using System;
using System.Collections.Generic;
using System.Text;
using UniversityActivities.Application.DTOs.Activities.Student;

namespace UniversityActivities.Application.UserCases.Activities.Student
{
    public interface IGenerateAttendanceCertificateUseCase
    {
        Task<AttendanceCertificateDto> ExecuteAsync(
        int studentId,
        int activityId);
    }
}
