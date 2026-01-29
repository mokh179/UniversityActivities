using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using UniversityActivities.Application.Common.Models;
using UniversityActivities.Application.DTOs.Activities;
using UniversityActivities.Application.Interfaces.Repositories.Activies.AdminActivies;
using UniversityActivities.Domain.Entities;
using UniversityActivities.Domain.Enums;

namespace UniversityActivities.Infrastructure.Persistence.Repositories.Activities.Admin
{
    public class AdminActivityRepository : IAdminActivityRepository
    {
        private readonly AppDbContext _context;

        public AdminActivityRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<int> CreateAsync(Activity activity)
        {
            _context.Activities.Add(activity);
            await _context.SaveChangesAsync();
            return activity.Id;
        }

        public async Task UpdateAsync(Activity activity)
        {
            _context.Activities.Update(activity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int activityId)
        {
            var activity = await _context.Activities
                .FirstOrDefaultAsync(x => x.Id == activityId);

            if (activity == null)
                return;

            activity.IsDeleted = true;
            await _context.SaveChangesAsync();
        }

        public Task SetPublishStatusAsync(int activityId, bool isPublished)
        {
            throw new NotImplementedException();
        }

        public async Task<PagedResult<ActivityAdminListItemDto>> GetAllAsync(ActivityAdminFilter filter, PagedRequest paging)
        {
            var query =
           from a in _context.Activities
           join m in _context.Managements on a.ManagementId equals m.Id
           join am in _context.AttendanceModes on a.AttendanceModeId equals am.Id
           join at in _context.ActivityTypes on a.ActivityTypeId equals at.Id
           where !a.IsDeleted
           select new
           {
               Activity = a,
               Management = m,
               AttendanceMode = am,
               ActivityType = at
           };
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
            if (filter.IsPublished.HasValue)
            {
                query = query.Where(x =>
                    x.Activity.IsPublished == filter.IsPublished);
            }
            var totalCount = await query.CountAsync();
            var items = await query
            .OrderByDescending(x => x.Activity.StartDateTime)
            .Skip((paging.PageNumber - 1) * paging.PageSize)
            .Take(paging.PageSize)
            .Select(x => new ActivityAdminListItemDto
            {
                Id = x.Activity.Id,
                TitleAr = x.Activity.TitleAr,
                TitleEn = x.Activity.TitleEn,
                ImageUrl = x.Activity.ImageUrl,
                IsPublished = x.Activity.IsPublished,
                StartDate = x.Activity.StartDateTime,
                EndDate = x.Activity.EndDateTime,

                ManagementNameAr = x.Management.NameAr,
                ManagementNameEn = x.Management.NameEn,

                AttendanceModeNameAr = x.AttendanceMode.NameAr,
                AttendanceModeNameEn = x.AttendanceMode.NameEn,

                ActivityTypeNameAr = x.ActivityType.NameAr,
                ActivityTypeNameEn = x.ActivityType.NameEn,
                RegisteredCount = _context.StudentActivities
                    .Count(sa => sa.ActivityId == x.Activity.Id),

                AttendedCount = _context.StudentActivities
                    .Count(sa =>
                        sa.ActivityId == x.Activity.Id &&
                        sa.AttendedAt != null),

                SupervisorsCount = _context.ActivityUsers
                    .Count(au =>
                        au.ActivityId == x.Activity.Id &&
                        au.ActivityRoleId == (int)ActivityRoles.Supervisor),

                CoordinatorsCount = _context.ActivityUsers
                    .Count(au =>
                        au.ActivityId == x.Activity.Id &&
                        au.ActivityRoleId == (int)ActivityRoles.Coordinator),

                ViewersCount = _context.ActivityUsers
                    .Count(au =>
                        au.ActivityId == x.Activity.Id &&
                        au.ActivityRoleId == (int)ActivityRoles.Viewer),
                ApproversCount = _context.ActivityUsers
                    .Count(au =>
                        au.ActivityId == x.Activity.Id &&
                        au.ActivityRoleId == (int)ActivityRoles.Approver)
            })
            .ToListAsync();
            return new PagedResult<ActivityAdminListItemDto>(
            items,
            totalCount,
            paging.PageNumber,
            paging.PageSize);
        }

 
        public async Task<ActivityAdminDetailsDto?> GetDetailsAsync(int activityId)
        {
            return await (
                from a in _context.Activities
                join m in _context.Managements on a.ManagementId equals m.Id
                join am in _context.AttendanceModes on a.AttendanceModeId equals am.Id
                join at in _context.ActivityTypes on a.ActivityTypeId equals at.Id
                where a.Id == activityId && !a.IsDeleted
                select new ActivityAdminDetailsDto
                {
                    Id = a.Id,
                    TitleAr = a.TitleAr,
                    TitleEn = a.TitleEn,
                    DescriptionAr = a.DescriptionAr,
                    DescriptionEn = a.DescriptionEn,
                    ImageUrl = a.ImageUrl,
                    IsPublished = a.IsPublished,

                    StartDate = a.StartDateTime,
                    EndDate = a.EndDateTime,

                    LocationEn = a.LocationEn,
                    LocationAr = a.LocationAr,
                    OnlineLink = a.OnlineLink,

                    ManagementNameAr = m.NameAr,
                    ManagementNameEn = m.NameEn,

                    AttendanceModeNameAr = am.NameAr,
                    AttendanceModeNameEn = am.NameEn,

                    ActivityTypeNameAr = at.NameAr,
                    ActivityTypeNameEn = at.NameEn
                }
            ).FirstOrDefaultAsync();
        }

        public async Task<Activity?> GetEntityAsync(int activityId)
        {
            return await _context.Activities
                .FirstOrDefaultAsync(x =>
                    x.Id == activityId &&
                    !x.IsDeleted);
        }
    }
}
