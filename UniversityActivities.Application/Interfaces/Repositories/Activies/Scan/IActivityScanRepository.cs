using System;
using System.Collections.Generic;
using System.Text;
using UniversityActivities.Domain.Entities;

namespace UniversityActivities.Application.Interfaces.Repositories.Activies.Scan
{
    public interface IActivityScanRepository
    {
        Task<Activity?> GetPublishedActivityAsync(
            int activityId,
            CancellationToken cancellationToken);

        Task<StudentActivity?> GetStudentActivityAsync(
            int activityId,
            int studentId,
            CancellationToken cancellationToken);

        Task<bool> HasEvaluationAsync(
            int activityId,
            int studentId,
            CancellationToken cancellationToken);

        Task RegisterAsync(
            StudentActivity studentActivity,
            CancellationToken cancellationToken);

        Task MarkAttendanceAsync(
            StudentActivity studentActivity,
            CancellationToken cancellationToken);

    }
}
