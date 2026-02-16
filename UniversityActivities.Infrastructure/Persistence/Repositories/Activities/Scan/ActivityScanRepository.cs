using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using UniversityActivities.Application.Interfaces.IUnitOfWork;
using UniversityActivities.Application.Interfaces.Repositories.Activies.Scan;
using UniversityActivities.Domain.Entities;

namespace UniversityActivities.Infrastructure.Persistence.Repositories.Activities.Scan
{
    public class ActivityScanRepository : IActivityScanRepository
    {
        private readonly AppDbContext _context;

        public ActivityScanRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Activity?> GetPublishedActivityAsync(
            int activityId,
            CancellationToken cancellationToken)
        {
            return await _context.Activities
                .FirstOrDefaultAsync(a =>
                    a.Id == activityId &&
                    a.IsPublished &&
                    !a.IsDeleted,
                    cancellationToken);
        }

        public async Task<StudentActivity?> GetStudentActivityAsync(
            int activityId,
            int studentId,
            CancellationToken cancellationToken)
        {
            return await _context.StudentActivities
                .FirstOrDefaultAsync(sa =>
                    sa.ActivityId == activityId &&
                    sa.StudentId == studentId,
                    cancellationToken);
        }

        public async Task<bool> HasEvaluationAsync(
            int activityId,
            int studentId,
            CancellationToken cancellationToken)
        {
            return await _context.ActivityEvaluations
                .AnyAsync(e =>
                    e.ActivityId == activityId &&
                    e.StudentId == studentId,
                    cancellationToken);
        }

        public async Task RegisterAsync(
            StudentActivity studentActivity,
            CancellationToken cancellationToken)
        {
            await _context.StudentActivities
                .AddAsync(studentActivity, cancellationToken);
        }

        public Task MarkAttendanceAsync(
            StudentActivity studentActivity,
            CancellationToken cancellationToken)
        {
            _context.StudentActivities.Update(studentActivity);
            return Task.CompletedTask;
        }

        public Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            return _context.SaveChangesAsync(cancellationToken);
        }
    }
}
