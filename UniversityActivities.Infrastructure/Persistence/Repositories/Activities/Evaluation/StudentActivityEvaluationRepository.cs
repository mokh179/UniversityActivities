using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using UniversityActivities.Application.DTOs.Evaluation;
using UniversityActivities.Application.Interfaces.Repositories.Activies.StudentActivies;

namespace UniversityActivities.Infrastructure.Persistence.Repositories.Activities.Evaluation
{
    public class StudentActivityEvaluationRepository:IStudentActivityEvaluationRepository
    {
        private readonly AppDbContext _context;
        public StudentActivityEvaluationRepository(AppDbContext context)
        {
            _context = context;
        }


        public async Task<bool> HasEvaluatedAsync(int studentId, int activityId)
        {
            return await _context.ActivityEvaluations
                .AnyAsync(x =>
                    x.StudentId == studentId &&
                    x.ActivityId == activityId);
        }


        public async Task SubmitAsync(
            int studentId,
            int activityId,
            List<EvaluationItemDto> items)
        {
            var entities = items.Select(item => new ActivityEvaluation
            {
                StudentId = studentId,
                ActivityId = activityId,
                EvaluationCriteriaId = item.EvaluationCrieteriaId,
                Value = item.Value,
                CreatedAt = DateTime.UtcNow
            });

            await _context.ActivityEvaluations.AddRangeAsync(entities);
        }
        public async Task SubmitCommentAsync(
     int studentId,
     int activityId,
     string comment)
        {
            var entity = new ActivityEvaluationComment
            {
                StudentId = studentId,
                ActivityId = activityId,
                Comment = comment,
                CreatedAt = DateTime.UtcNow
            };

            _context.ActivityEvaluationComments.Add(entity);
        }
    }
}
