using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using UniversityActivities.Application.DTOs.Activities;

namespace UniversityActivities.Infrastructure.Persistence.Repositories.Activities.Admin
{
    public class ActivityAssignmentRepository:IActivityAssignmentRepository
    {
        private readonly AppDbContext _context;

        public ActivityAssignmentRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task ReplaceAsync(
        int activityId,
        List<ActivityAssignmentDto> assignments)
        {
            //  Remove existing assignments
            await _context.ActivityUsers
                .Where(x => x.ActivityId == activityId)
                .ExecuteDeleteAsync();

            //  Add new assignments
            if (assignments == null || assignments.Count == 0)
                return;
            var entities = assignments
           .GroupBy(x => new { x.UserId, x.ActivityRoleId })
           .Select(g => g.First())
           .Select(x => new ActivityUser
           {
               ActivityId = activityId,
               UserId = x.UserId,
               ActivityRoleId = x.ActivityRoleId
           })
           .ToList();

            await _context.ActivityUsers.AddRangeAsync(entities);
        }

    }
}
