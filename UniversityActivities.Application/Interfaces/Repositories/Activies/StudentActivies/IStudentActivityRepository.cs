using System;
using System.Collections.Generic;
using System.Text;

namespace UniversityActivities.Application.Interfaces.Repositories.Activies.StudentActivies
{
    public interface IStudentActivityRepository
    {
        Task RegisterAsync(int studentId, int activityId);

        Task MarkAttendanceAsync(int studentId, int activityId);

        Task<bool> IsRegisteredAsync(int studentId, int activityId);
        Task<bool> HasAttendedAsync(int studentId, int activityId);
    }
}
