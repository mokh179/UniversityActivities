using System;
using System.Collections.Generic;
using System.Text;

namespace UniversityActivities.Application.UserCases.Activities.Student
{
    public interface IMarkStudentAttendanceUseCase
    {
        Task ExecuteAsync(int studentId, int activityId);
    }
}
