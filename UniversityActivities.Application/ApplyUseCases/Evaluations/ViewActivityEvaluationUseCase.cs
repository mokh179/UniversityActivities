using System;
using System.Collections.Generic;
using System.Text;
using UniversityActivities.Application.Common.Models;
using UniversityActivities.Application.DTOs.Activities;
using UniversityActivities.Application.DTOs.Evaluation;
using UniversityActivities.Application.Interfaces.Repositories.Activies.AdminActivies;
using UniversityActivities.Application.UserCases.Activities.Admin;
using UniversityActivities.Application.UserCases.Activities.Student;

namespace UniversityActivities.Application.ApplyUseCases.Evaluations
{
    public class ViewActivityEvaluationUseCase : IViewAdminActivitiesUseCase
    {
        private readonly IAdminActivityEvaluationQueryRepository _evaluationQueryRepository;

        public ViewActivityEvaluationUseCase(
            IAdminActivityEvaluationQueryRepository evaluationQueryRepository)
        {
            _evaluationQueryRepository = evaluationQueryRepository;
        }

        public Task<PagedResult<ActivityAdminListItemDto>> ExecuteAsync(ActivityAdminFilter filter, PagedRequest paging)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ActivityEvaluationCriteriaAverageDto>> GetCriteriaAveragesAsync(int activityId)
        {
            return await _evaluationQueryRepository.GetCriteriaAveragesAsync(activityId);
        }

        public async Task<ActivityEvaluationSummaryDto> GetSummaryAsync(int activityId)
        {
            return await _evaluationQueryRepository.GetSummaryAsync(activityId);
        }
    }
}
