using System;
using System.Collections.Generic;
using System.Text;
using UniversityActivities.Application.Common.Models;
using UniversityActivities.Application.DTOs.Activities;
using UniversityActivities.Application.DTOs.Evaluation;
using UniversityActivities.Application.Interfaces.Repositories.Activies.AdminActivies;
using UniversityActivities.Application.UserCases.Activities.Admin;
using UniversityActivities.Application.UserCases.Activities.Student;

namespace UniversityActivities.Application.Implementation.ApplyUseCases.Evaluations
{
    public class ViewActivityEvaluationUseCase : IViewActivityEvaluationUseCase
    {
        private readonly IAdminActivityEvaluationQueryRepository _evaluationQueryRepository;

        public ViewActivityEvaluationUseCase(
            IAdminActivityEvaluationQueryRepository evaluationQueryRepository)
        {
            _evaluationQueryRepository = evaluationQueryRepository;
        }



        public async Task<List<ActivityEvaluationCriteriaAverageDto>> GetCriteriaAsync(int activityId)
        {
            return await _evaluationQueryRepository.GetCriteriaAveragesAsync(activityId);
        }

        public async Task<ActivityEvaluationSummaryDto> GetSummaryAsync(int activityId)
        {
            return await _evaluationQueryRepository.GetSummaryAsync(activityId);
        }

        //public async Task<List<ActivityEvaluationCriteriaAverageDto>> GetCriteriaAveragesAsync(int activityId)
        //{
        //    return await _evaluationQueryRepository.GetCriteriaAveragesAsync(activityId);
        //}

        //public async Task<ActivityEvaluationSummaryDto> GetSummaryAsync(int activityId)
        //{
        //    return await _evaluationQueryRepository.GetSummaryAsync(activityId);
        //}
    }
}
