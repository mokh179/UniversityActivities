using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using UniversityActivities.Application.DTOs.Evaluation;
using UniversityActivities.Application.Interfaces.Repositories.Activies.AdminActivies;

namespace UniversityActivities.Infrastructure.Persistence.Repositories.Activities.Evaluation
{
    public class AdminActivityEvaluationQueryRepository:IAdminActivityEvaluationQueryRepository
    {
        private readonly AppDbContext _context;
        public AdminActivityEvaluationQueryRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<ActivityEvaluationCriteriaAverageDto>> GetCriteriaAveragesAsync(
         int activityId)
        {
            return await (
                from e in _context.ActivityEvaluations
                join c in _context.EvaluationCriteria
                    on e.EvaluationCriteriaId equals c.Id
                where e.ActivityId == activityId
                group e by new
                {
                    e.EvaluationCriteriaId,
                    c.NameAr,
                    c.NameEn
                }
                into g
                select new ActivityEvaluationCriteriaAverageDto
                {
                    EvaluationCriteriaId = g.Key.EvaluationCriteriaId,
                    CriteriaNameAr = g.Key.NameAr,
                    CriteriaNameEn = g.Key.NameEn,
                    AverageValue = Math.Round(g.Average(x => x.Value), 2)
                }
            ).ToListAsync();
        }

        public async Task<ActivityEvaluationSummaryDto> GetSummaryAsync(int activityId)
        {
            var query = _context.ActivityEvaluations
                .Where(x => x.ActivityId == activityId);

            var totalEvaluations = await query
                .Select(x => x.StudentId)
                .Distinct()
                .CountAsync();

            var overallAverage = await query
                .AverageAsync(x => (double?)x.Value) ?? 0;

            return new ActivityEvaluationSummaryDto
            {
                ActivityId = activityId,
                TotalEvaluations = totalEvaluations,
                OverallAverage = Math.Round(overallAverage, 2)
            };
        }
    }
}
