using System;
using System.Collections.Generic;
using System.Text;
using UniversityActivities.Application.DTOs.Evaluation;

namespace UniversityActivities.Application.Interfaces.Repositories.Activies.AdminActivies
{
    public interface IAdminActivityEvaluationQueryRepository
    {
        Task<ActivityEvaluationSummaryDto> GetSummaryAsync(
     int activityId);

        Task<List<ActivityEvaluationCriteriaAverageDto>> GetCriteriaAveragesAsync(
            int activityId);
    }
}
