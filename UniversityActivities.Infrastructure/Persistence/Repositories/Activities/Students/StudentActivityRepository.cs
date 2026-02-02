using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using UniversityActivities.Application.Interfaces.Repositories.Activies.StudentActivies;

namespace UniversityActivities.Infrastructure.Persistence.Repositories.Activities.Students
{
    public class StudentActivityRepository : IStudentActivityRepository

    {
        private readonly AppDbContext _context;

        public StudentActivityRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task RegisterAsync(int studentId, int activityId)
        {
            var entity = new StudentActivity
            {
                StudentId = studentId,
                ActivityId = activityId,
                RegisteredAt = DateTime.UtcNow
            };

            _context.StudentActivities.Add(entity);
        }
        public async Task MarkAttendanceAsync(int studentId, int activityId)
        {
            await _context.StudentActivities
                .Where(x =>
                    x.StudentId == studentId &&
                    x.ActivityId == activityId)
                .ExecuteUpdateAsync(setters =>
                    setters.SetProperty(x => x.AttendedAt, DateTime.UtcNow));
        }
        public async Task<bool> IsRegisteredAsync(int studentId, int activityId)
        {
            return await _context.StudentActivities
                .AnyAsync(x =>
                    x.StudentId == studentId &&
                    x.ActivityId == activityId);
        }
        public async Task<bool> HasAttendedAsync(int studentId, int activityId)
        {
            return await _context.StudentActivities
                .AnyAsync(x =>
                    x.StudentId == studentId &&
                    x.ActivityId == activityId && x.AttendedAt != null);
        }

    }
}
