using System;
using System.Collections.Generic;
using System.Text;
using UniversityActivities.Application.DTOs.Evaluation;

namespace UniversityActivities.Application.UserCases.Activities.Admin
{
    public interface IViewActivityEvaluationUseCase
    {
        Task<ActivityEvaluationSummaryDto> GetSummaryAsync(
       int activityId);

        Task<List<ActivityEvaluationCriteriaAverageDto>> GetCriteriaAsync(
            int activityId);
    }
}
