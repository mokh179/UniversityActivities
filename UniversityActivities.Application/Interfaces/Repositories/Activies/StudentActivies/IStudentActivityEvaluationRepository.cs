using System;
using System.Collections.Generic;
using System.Text;
using UniversityActivities.Application.DTOs.Evaluation;

namespace UniversityActivities.Application.Interfaces.Repositories.Activies.StudentActivies
{
    public interface IStudentActivityEvaluationRepository
    {
        Task SubmitAsync(
            int studentId,
            int activityId,
            List<EvaluationItemDto> items);

        Task<bool> HasEvaluatedAsync(
            int studentId,
            int activityId);
    }
}
