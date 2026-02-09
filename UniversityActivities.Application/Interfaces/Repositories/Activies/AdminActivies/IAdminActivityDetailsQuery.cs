using System;
using System.Collections.Generic;
using System.Text;
using UniversityActivities.Application.DTOs.Activities.ActivityParticipationView;

namespace UniversityActivities.Application.Interfaces.Repositories.Activies.AdminActivies
{
    public interface IAdminActivityDetailsQuery
    {
        // Cards 
        Task<ActivityAttendanceStatsDto> GetActivityStatsAsync(int activityId);

        // Table (Registered Users + Search)
        Task<List<ActivityParticipantDto>> GetParticipantsAsync(
            int activityId,
            string? search = null);

        // Modal (View Evaluation)
        Task<StudentEvaluationModalDto?> GetStudentEvaluationAsync(
            int activityId,
            int studentId);

        // Certificate (Check availability)
        Task<bool> CanViewCertificateAsync(
            int activityId,
            int studentId);
    }

}
