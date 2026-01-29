using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using UniversityActivities.Application.Common.Models;
using UniversityActivities.Application.DTOs.Activities.Student;
using UniversityActivities.Application.Interfaces.Repositories.Activies.StudentActivies;

namespace UniversityActivities.Infrastructure.Persistence.Repositories.Activities.Students
{
    public class StudentActivityQueryRepository: IStudentActivityQueryRepository
    {
        private readonly AppDbContext _context;
        public StudentActivityQueryRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<StudentActivityDetailsDto?> GetDetailsAsync(int activityId, int studentId)
        {
            return await (
            from a in _context.Activities.AsNoTracking()
            join m in _context.Managements on a.ManagementId equals m.Id
            where a.Id == activityId && a.IsPublished
            select new StudentActivityDetailsDto
            {
                Id = a.Id,
                TitleAr = a.TitleAr,
                TitleEn = a.TitleEn,
                DescriptionAr = a.DescriptionAr,
                DescriptionEn = a.DescriptionEn,
                ImageUrl = a.ImageUrl,

                StartDate = a.StartDateTime,
                EndDate = a.EndDateTime,

                Location = string.IsNullOrEmpty(a.LocationEn) ?a.LocationAr:a.LocationEn,
                OnlineLink = a.OnlineLink,

                ManagementNameAr = m.NameAr,
                ManagementNameEn = m.NameEn
            }
        ).FirstOrDefaultAsync();
        }

        public async Task<PagedResult<StudentActivityListItemDto>> GetPublishedActivitiesAsync(int studentId, int studentManagementId, List<int> studentTargetAudienceIds, StudentActivityFilter filter, PagedRequest paging)
        {
            var query =
            from a in _context.Activities.AsNoTracking()
            join m in _context.Managements on a.ManagementId equals m.Id
            where a.IsPublished
            select new
            {
                Activity = a,
                Management = m
            };

            // =========================
            // Filters
            // =========================

            if (!string.IsNullOrWhiteSpace(filter.Title))
            {
                query = query.Where(x =>
                    x.Activity.TitleAr.Contains(filter.Title) ||
                    x.Activity.TitleEn.Contains(filter.Title));
            }

            if (filter.ManagementId.HasValue)
            {
                query = query.Where(x =>
                    x.Activity.ManagementId == filter.ManagementId);
            }

            if (filter.AttendanceModeId.HasValue)
            {
                query = query.Where(x =>
                    x.Activity.AttendanceModeId == filter.AttendanceModeId);
            }

            if (filter.StartDateFrom.HasValue)
            {
                query = query.Where(x =>
                    x.Activity.StartDateTime >= filter.StartDateFrom);
            }

            if (filter.StartDateTo.HasValue)
            {
                query = query.Where(x =>
                    x.Activity.StartDateTime <= filter.StartDateTo);
            }

            // =========================
            // Target Audience Logic
            // =========================
            query = query.Where(x =>
                _context.ActivityTargetAudiences.Any(ta =>
                    ta.ActivityId == x.Activity.Id &&
                    studentTargetAudienceIds.Contains(ta.TargetAudienceId)));

            var totalCount = await query.CountAsync();

            var items = await query
                .OrderBy(x => x.Activity.StartDateTime)
                .Skip((paging.PageNumber - 1) * paging.PageSize)
                .Take(paging.PageSize)
                .Select(x => new StudentActivityListItemDto
                {
                    Id = x.Activity.Id,
                    TitleAr = x.Activity.TitleAr,
                    TitleEn = x.Activity.TitleEn,
                    ImageUrl = x.Activity.ImageUrl,
                    StartDate = x.Activity.StartDateTime,
                    EndDate = x.Activity.EndDateTime,

                    ManagementNameAr = x.Management.NameAr,
                    ManagementNameEn = x.Management.NameEn,

                    IsRegistered = _context.StudentActivities.Any(sa =>
                        sa.ActivityId == x.Activity.Id &&
                        sa.StudentId == studentId)
                })
                .ToListAsync();

            return new PagedResult<StudentActivityListItemDto>(
                items,
                totalCount,
                paging.PageNumber,
                paging.PageSize);

        }
    }
}
