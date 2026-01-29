using System;
using System.Collections.Generic;
using System.Text;
using UniversityActivities.Application.DTOs.Evaluation;
using UniversityActivities.Application.Interfaces.Repositories.Activies.AdminActivies;
using UniversityActivities.Application.UserCases.Activities.Student;

namespace UniversityActivities.Application.ApplyUseCases.Evaluations
{
    public class ViewActivityEvaluationUseCase : IAdminActivityEvaluationQueryRepository
    {
        public Task<List<ActivityEvaluationCriteriaAverageDto>> GetCriteriaAveragesAsync(int activityId)
        {
            throw new NotImplementedException();
        }

        public Task<ActivityEvaluationSummaryDto> GetSummaryAsync(int activityId)
        {
            throw new NotImplementedException();
        }
    }
}
