using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
//using System.Diagnostics;
using System.Text;
using UniversityActivities.Application.AuthorizationModule.Models;
using UniversityActivities.Application.Common.Models;
using UniversityActivities.Application.DTOs.Activities;
using UniversityActivities.Application.DTOs.Enums;
using UniversityActivities.Application.Exceptions;
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
;
        }

        public async Task<int> CreateAsync(Activity activity)
        {
            try
            {
                _context.Activities.Add(activity);
                await _context.SaveChangesAsync();
                return activity.Id;
            }
            catch (Exception ex)
            {

                throw new BusinessException("Error occured");
            }
           
        }

        public async Task UpdateAsync(Activity activity)
        {
            try
            {
                _context.Activities.Update(activity);
                // await _context.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw new BusinessException("Error occured");
            }
        }

        public async Task DeleteAsync(int activityId)
        {
            var activity = await _context.Activities
                .FirstOrDefaultAsync(x => x.Id == activityId);

            if (activity == null)
                return;

            activity.IsDeleted = true;
            //await _context.SaveChangesAsync();
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
            if (filter != null) 
            {
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
                status=(StatusEnums)x.Activity.ActivityStatusId,
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
            paging.PageSize, paging.PageSize == 0 ? 1 : (int)Math.Ceiling(totalCount / (double)paging.PageSize));
        }

 
        public async Task<ActivityAdminDetailsDto?> GetDetailsAsync(int activityId)
        {
            try
            {
                var res = await (
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
                                ActivityTypeNameEn = at.NameEn,

                                ClubNameAr = a.StudentClub.NameAr,
                                ClubNameEn = a.StudentClub.NameEn,

                                TargetAudiencesAr = a.ActivityTargetAudiences
                                    .Select(ata => ata.TargetAudience.NameAr)
                                    .ToList(),

                                TargetAudiencesEn = a.ActivityTargetAudiences
                                    .Select(ata => ata.TargetAudience.NameEn)
                                    .ToList(),

                                Coordinators =
                                    (from au in _context.ActivityUsers
                                     join u in _context.Users on au.UserId equals u.Id
                                     where au.ActivityId == a.Id
                                        && au.ActivityRoleId == (int)ActivityRoles.Coordinator
                                     select new ActivityUserViewDto
                                     {
                                         UserId = u.Id,
                                         FullNameEn = u.FirstName + " " + u.LastName
                                     }).ToList(),

                                Viewers =
                                    (from au in _context.ActivityUsers
                                     join u in _context.Users on au.UserId equals u.Id
                                     where au.ActivityId == a.Id
                                        && au.ActivityRoleId == (int)ActivityRoles.Viewer
                                     select new ActivityUserViewDto
                                     {
                                         UserId = u.Id,
                                         FullNameEn = u.FirstName + " " + u.LastName
                                     }).ToList(),

                                Supervisors =
                                    (from au in _context.ActivityUsers
                                     join u in _context.Users on au.UserId equals u.Id
                                     where au.ActivityId == a.Id
                                        && au.ActivityRoleId == (int)ActivityRoles.Supervisor
                                     select new ActivityUserViewDto
                                     {
                                         UserId = u.Id,
                                         FullNameEn = u.FirstName + " " + u.LastName
                                     }).ToList(),
                            }
                        ).FirstOrDefaultAsync();


                return res;  
            }
            catch (Exception ex)
            {

                throw new BusinessException("An error occured ...");
            }

        }

        public async Task<Activity?> GetEntityAsync(int activityId)
        {
            return await _context.Activities.Include(a => a.ActivityTargetAudiences)
        .Include(a => a.ActivityUsers)
                .FirstOrDefaultAsync(x =>
                    x.Id == activityId &&
                    !x.IsDeleted);
        }



        public async Task<AdminStatistics> GetAdminStatisticsAsync(int? mangementId)
        {
            
            var Allactivities=new List<Activity>();
            if (mangementId == 0|| mangementId==null) 
                 Allactivities= await _context.Activities.ToListAsync();
            else
                 Allactivities = await  _context.Activities.Where(x => x.ManagementId == mangementId).ToListAsync();
            return new AdminStatistics
                {
                    TotalActivities =  Allactivities.Count(),
                    UpcomingActivities =  Allactivities.Count(x => x.ActivityStatusId == 3),
                    CompletedActivities =  Allactivities.Count(x => x.ActivityStatusId == 2),
                    InProgressActivities =  Allactivities.Count(x => x.ActivityStatusId == 1),
                };

           
        }



        public async Task<ActivityParticipants> GetActivityParticipants(int activityId)
        {
            var Result = new ActivityParticipants();
            try
            {
                var query =
                        from a in _context.Activities
                        join AU in _context.ActivityUsers on a.Id equals AU.ActivityId
                        join U in _context.Users on AU.UserId equals U.Id
                        where a.Id == activityId && !a.IsDeleted
                        select new
                        {
                            ActivityUser = AU,
                            User = U
                        };

                Result.Coordinators = await query.Where(x => x.ActivityUser.ActivityRoleId == (int)ActivityRoles.Coordinator).Select(a => new ParticipantsDto
                {
                    Id = a.User.Id,
                    RoleName = ActivityRoles.Coordinator.ToString(),
                    Useraname = a.User.UserName,
                    Fullname = a.User.FirstName + " " + a.User.LastName
                }).ToListAsync();
                Result.Viewer = await query.Where(x => x.ActivityUser.ActivityRoleId == (int)ActivityRoles.Viewer).Select(a => new ParticipantsDto
                {
                    Id = a.User.Id,
                    RoleName = ActivityRoles.Coordinator.ToString(),
                    Useraname = a.User.UserName,
                    Fullname = a.User.FirstName + " " + a.User.LastName
                }).ToListAsync();
                Result.Supervisors = await query.Where(x => x.ActivityUser.ActivityRoleId == (int)ActivityRoles.Supervisor).Select(a => new ParticipantsDto
                {
                    Id = a.User.Id,
                    RoleName = ActivityRoles.Coordinator.ToString(),
                    Useraname = a.User.UserName,
                    Fullname = a.User.FirstName + " " + a.User.LastName
                }).ToListAsync();

                return Result;
            }
            catch (Exception ex)
            {

                throw new BusinessException("An error occured ...");
            }
                
                
        }

        
    }
}
