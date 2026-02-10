using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using UniversityActivities.Application.DTOs.Enums;
using UniversityActivities.Application.Interfaces.Repositories.Activies.Background;
using UniversityActivities.Domain.Entities;

namespace UniversityActivities.Infrastructure.Persistence.Repositories.Activities.BackgroundService
{
    public class ActivityStatusRepository:IActivityStatusRepository
    {
        private readonly AppDbContext _context;

        public ActivityStatusRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Activity>> GetUpcomingToStartAsync(
            DateTime now,
            CancellationToken cancellationToken)
        {
            return await _context.Activities
                .Where(a =>
                    a.ActivityStatusId == (int)StatusEnums.Upcoming &&
                    a.StartDateTime <= now &&
                    a.EndDateTime > now)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<Activity>> GetInProgressToCompleteAsync(
            DateTime now,
            CancellationToken cancellationTntoken)
        {
            return await _context.Activities
                .Where(a =>
                    a.ActivityStatusId == (int)StatusEnums.Inprogress &&
                    a.EndDateTime <= now)
                .ToListAsync(cancellationTntoken);
        }

        public async Task UpdateStatusAsync(
            IEnumerable<Activity> activities,
            StatusEnums newStatus,
            CancellationToken cancellationToken)
        {
            foreach (var activity in activities)
            {
                activity.ActivityStatusId = (int)newStatus;
            }

            //if (activities.Any())
            //{
            //    await _context.SaveChangesAsync(cancellationToken);
            //}
        }
    }
}
