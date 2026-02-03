using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using UniversityActivities.Application.DTOs.Admin;
using UniversityActivities.Application.Interfaces.Repositories.Admin;

namespace UniversityActivities.Infrastructure.Persistence.Repositories.Admin
{
    public class AdminDashboardQueryRepository:IDashboardQueryRepository
    {
        private readonly AppDbContext _context;

        public AdminDashboardQueryRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<AdminDashboardStatsDto> GetStatsAsync(int managementId)
        {
            var now = DateTime.UtcNow;

            return new AdminDashboardStatsDto
            {
                TotalActivities = await _context.Activities
                    .CountAsync(x => x.ManagementId == managementId),

                PublishedActivities = await _context.Activities
                    .CountAsync(x =>
                        x.ManagementId == managementId &&
                        x.IsPublished),

                ActiveActivities = await _context.Activities
                    .CountAsync(x =>
                        x.ManagementId == managementId &&
                        x.StartDateTime <= now &&
                        x.EndDateTime >= now),

                InProgressActivities = await _context.Activities
              .CountAsync(x =>
                x.ManagementId == managementId &&
                x.StartDateTime <= now &&
                x.EndDateTime > now),

                TotalStudents = await _context.StudentActivities
                    .Where(x => x.Activity.ManagementId == managementId)
                    .Select(x => x.StudentId)
                    .Distinct()
                    .CountAsync()
            };
        }

        public async Task<AdminDashboardStatsDto> GetGlobalStatsAsync()
        {
            var now = DateTime.UtcNow;

            return new AdminDashboardStatsDto
            {
                TotalActivities = await _context.Activities.CountAsync(),

                PublishedActivities = await _context.Activities
                    .CountAsync(x => x.IsPublished),

                ActiveActivities = await _context.Activities
                    .CountAsync(x =>
                        x.StartDateTime <= now &&
                        x.EndDateTime >= now),

                InProgressActivities = await _context.Activities
            .CountAsync(x =>
                x.StartDateTime <= now &&
                x.EndDateTime > now),

                TotalStudents = await _context.StudentActivities
                    .Select(x => x.StudentId)
                    .Distinct()
                    .CountAsync()
            };
        }
    }
}
