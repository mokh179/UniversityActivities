using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using UniversityActivities.Domain.Entities;

namespace UniversityActivities.Infrastructure.Persistence.Repositories.Activities.Admin
{
    public class ActivityTargetAudienceRepository:IActivityTargetAudienceRepository
    {
        private readonly AppDbContext _context;

        public ActivityTargetAudienceRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task ReplaceAsync(int activityId, List<int> targetAudienceIds)
        {
            // 🔹 Remove existing links
            await _context.ActivityTargetAudiences.AsNoTracking()
                .Where(x => x.ActivityId == activityId)
                .ExecuteDeleteAsync();

            // 🔹 Add new links
            if (targetAudienceIds == null || targetAudienceIds.Count == 0)
                return;

            var entities = targetAudienceIds
                .Distinct()
                .Select(targetAudienceId => new ActivityTargetAudience
                {
                    ActivityId = activityId,
                    TargetAudienceId = targetAudienceId
                })
                .ToList();

            await _context.ActivityTargetAudiences.AddRangeAsync(entities);
        }
    }
}
