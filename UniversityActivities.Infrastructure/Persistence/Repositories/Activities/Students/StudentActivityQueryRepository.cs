using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using UniversityActivities.Application.Common.Models;
using UniversityActivities.Application.DTOs.Activities.Student;
using UniversityActivities.Application.Interfaces.Repositories.Activies.StudentActivies;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
                ManagementNameEn = m.NameEn,
                ScopeAr = a.AttendanceScope.NameAr,
                ScopeEn = a.AttendanceScope.NameEn,
                ActivityTypeAr = a.ActivityType.NameAr,
                ActivityTypeEn = a.ActivityType.NameEn, 
                TargetAudiencesAr = (from ta in _context.ActivityTargetAudiences
                                     join t in _context.TargetAudiences on ta.TargetAudienceId equals t.Id
                                     where ta.ActivityId == a.Id
                                     select t.NameAr).ToList(),
                TargetAudiencesEn = (from ta in _context.ActivityTargetAudiences
                                     join t in _context.TargetAudiences on ta.TargetAudienceId equals t.Id
                                     where ta.ActivityId == a.Id
                                     select t.NameEn).ToList(),
                IsRegistered = _context.StudentActivities.Any(sa =>
                            sa.ActivityId == a.Id &&
                            sa.StudentId == studentId),
                IsAttended = _context.StudentActivities.Any(sa =>
                            sa.ActivityId == a.Id &&
                            sa.StudentId == studentId&&sa.AttendedAt!=null),
                IsRated= _context.ActivityEvaluations.Count(sa =>
                            sa.ActivityId == a.Id &&
                            sa.StudentId == studentId)==5,

                ActivityFinished = a.EndDateTime < DateTime.UtcNow

            }).FirstOrDefaultAsync();
        }


        public async Task<StudentActivityCertificateDto?> GetCertificateDetails(int activityId, int studentId)
        {
            try
            {

                
                var data = await (
                from a in _context.Activities.AsNoTracking()
                join m in _context.Managements on a.ManagementId equals m.Id
                join sa in _context.StudentActivities on a.Id equals sa.ActivityId
                join u in _context.Users on sa.StudentId equals u.Id
                where a.Id == activityId && a.IsPublished==true && sa.StudentId == studentId
                select new 
                {
                    StudentName=u.FirstName + '\t' + u.MiddleName + '\t' + u.LastName,
                    a.TitleEn,
                     m.NameEn,
                     a.StartDateTime
                }).FirstOrDefaultAsync();
                if (data != null)
                {
                        return new StudentActivityCertificateDto
                        {
                            StudentName = data.StudentName,
                            ActivityTitle = data.TitleEn,
                            ManagementName = data.NameEn,
                            ActivityDate = data.StartDateTime.ToString("MM/dd/yyyy")
                        };

                }
                return null;
                //return await (
                //from a in _context.Activities.AsNoTracking()
                //join m in _context.Managements on a.ManagementId equals m.Id
                //join sa in _context.StudentActivities on a.Id equals sa.ActivityId
                //join u in _context.Users on sa.StudentId equals u.Id
                //where a.Id == activityId && a.IsPublished&& sa.StudentId == studentId
                //select new StudentActivityCertificateDto
                //{
                //    StudentName = u.FirstName +'\t'+u.MiddleName + '\t'+u.LastName,
                //    ActivityTitle = a.TitleEn,
                //    ManagementName = m.NameEn,
                //    ActivityDate = a.StartDateTime.ToString("MMMM dd, yyyy")
                //}).FirstOrDefaultAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }


        public async Task<PagedResult<StudentActivityListItemDto>> GetPublishedActivitiesAsync(int studentid, List<int> studentTargetAudienceIds, StudentActivityFilter filter, PagedRequest paging)
        {
            var query =
            from a in _context.Activities.AsNoTracking()
            join at in _context.ActivityTypes on a.ActivityTypeId equals at.Id
            join actas in _context.ActivityStatuses on a.ActivityStatusId equals actas.Id
            join m in _context.Managements on a.ManagementId equals m.Id
            //join c in _context.Clubs on a.StudentClubId equals c.Id
            where a.IsPublished
            select new
            {
                Activity = a,
                Management = m,
               // Club=c,
                ActivityTypes=at,
                ActivityStatuses= actas
            };

            // =========================
            // Filters
            // =========================
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
                if (filter.Activitytype.HasValue)
                {
                    query = query.Where(x =>
                        x.Activity.AttendanceModeId == filter.Activitytype);
                }
                if (filter.AttendanceModeId.HasValue)
                {
                    query = query.Where(x =>
                        x.Activity.AttendanceModeId == filter.AttendanceModeId);
                }

                if (filter.AttendanceScopeId.HasValue)
                {
                    query = query.Where(x =>
                        x.Activity.AttendanceScopeId == filter.AttendanceScopeId);
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
            }

            // =========================
            // Target Audience Logic
            // =========================
            query = query.Where(x =>
                _context.ActivityTargetAudiences.Any(ta =>
                    ta.ActivityId == x.Activity.Id &&
                    studentTargetAudienceIds.Contains(ta.TargetAudienceId)));

            var totalCount = await query.CountAsync();
            if (totalCount > 0) 
            {

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
                        onlineLink=x.Activity.OnlineLink,
                        LocationAr=x.Activity.LocationAr,
                        LocationEn = x.Activity.LocationEn,
                        ManagementNameAr = x.Management.NameAr,
                        ManagementNameEn = x.Management.NameEn,
                        ActiviyTypeAr=x.ActivityTypes.NameAr,
                        ActiviyTypeEn=x.ActivityTypes.NameEn,
                        StatusAr=x.ActivityStatuses.NameAr,
                        StatusEn=x.ActivityStatuses.NameEn,
                        IsRegistered = _context.StudentActivities.Any(sa =>
                            sa.ActivityId == x.Activity.Id &&
                            sa.StudentId == studentid)
                    })
                    .ToListAsync();
                return new PagedResult<StudentActivityListItemDto>(
                    items,
                    totalCount,
                    paging.PageNumber,
                    paging.PageSize, paging.PageSize == 0 ? 1 : (int)Math.Ceiling(totalCount / (double)paging.PageSize));
            }
            return new PagedResult<StudentActivityListItemDto>(
              new List<StudentActivityListItemDto>(),
              totalCount,
              paging.PageNumber,
              paging.PageSize,0);

        }
    }
}
