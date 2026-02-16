using System;
using System.Collections.Generic;
using System.Text;

namespace UniversityActivities.Application.UserCases.Activities.Student
{
    public interface IRegisterStudentInActivityUseCase
    {
        Task ExecuteAsync(int studentId, int activityId);
    }
}
