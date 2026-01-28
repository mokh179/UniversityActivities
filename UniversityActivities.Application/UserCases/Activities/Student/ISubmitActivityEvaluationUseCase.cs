using System;
using System.Collections.Generic;
using System.Text;
using UniversityActivities.Application.DTOs.Evaluation;

namespace UniversityActivities.Application.UserCases.Activities.Student
{
    public interface ISubmitActivityEvaluationUseCase
    {
        Task ExecuteAsync(
            int studentId,
            int activityId,
            List<EvaluationItemDto> items);
    }
}
