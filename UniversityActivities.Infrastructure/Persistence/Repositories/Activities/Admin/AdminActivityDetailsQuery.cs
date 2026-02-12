using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;
using UniversityActivities.Application.AuthorizationModule.Models.AuthModels;
using UniversityActivities.Application.Common.Models;
using UniversityActivities.Application.DTOs.Activities;
using UniversityActivities.Application.DTOs.Activities.ActivityParticipationView;
using UniversityActivities.Application.Interfaces.Repositories.Activies.AdminActivies;

namespace UniversityActivities.Infrastructure.Persistence.Repositories.Activities.Admin
{
    public class AdminActivityDetailsQuery
    : IAdminActivityDetailsQuery
    {
        private readonly AppDbContext _context;

        public AdminActivityDetailsQuery(AppDbContext context)
        {
            _context = context;
        }

        // =========================
        // Cards (Stats)
        // =========================
        public async Task<ActivityAttendanceStatsDto>
            GetActivityStatsAsync(int activityId)
        {
            var registeredCount = await _context.StudentActivities
                .CountAsync(sa => sa.ActivityId == activityId);

            var attendedCount = await _context.StudentActivities
                .CountAsync(sa =>
                    sa.ActivityId == activityId &&
                    sa.AttendedAt != null);

            var participatedCount = await _context.ActivityEvaluations
                .Where(e => e.ActivityId == activityId)
                .Select(e => e.StudentId)
                .Distinct()
                .CountAsync();

            var overallRate = await _context.ActivityEvaluations
           .Where(e => e.ActivityId == activityId)
           .Select(e => (double?)e.Value)
           .AverageAsync() ?? 0;

            return new ActivityAttendanceStatsDto
            {
                RegisteredCount = registeredCount,
                AttendedCount = attendedCount,
                ParticipatedCount = participatedCount,
                OverallAverageRate = Math.Round(overallRate, 2)
            };
        }

        // =========================
        // Table: Participants
        // =========================
        public async Task<PagedResult<ActivityParticipantDto>> GetParticipantsAsync(ActivityParticipantFilter filter)
        {
            var baseQuery =
              from sa in _context.StudentActivities
               join u in _context.Users
                   on sa.StudentId equals u.Id
               join m in _context.Managements
                   on u.ManagementId equals m.Id
               where sa.ActivityId == filter.ActivityId
               select new { sa, u, m };

            if (!string.IsNullOrWhiteSpace(filter.Search))
            {
                baseQuery = baseQuery.Where(x =>
                    x.u.FirstName.Contains(filter.Search) || x.u.MiddleName.Contains(filter.Search) || x.u.LastName.Contains(filter.Search));
            }

            if (filter.IsAttended.HasValue)
            {
                baseQuery = filter.IsAttended.Value
                    ? baseQuery.Where(x => x.sa.AttendedAt != null)
                    : baseQuery.Where(x => x.sa.AttendedAt == null);
            }

            if (filter.HasEvaluated.HasValue)
            {
                baseQuery = filter.HasEvaluated.Value
                    ? baseQuery.Where(x =>
                        _context.ActivityEvaluations.Any(e =>
                            e.ActivityId == filter.ActivityId &&
                            e.StudentId == x.sa.StudentId))
                    : baseQuery.Where(x =>
                        !_context.ActivityEvaluations.Any(e =>
                            e.ActivityId == filter.ActivityId &&
                            e.StudentId == x.sa.StudentId));
            }

            var totalCount = await baseQuery.CountAsync();

            var items = await baseQuery
                .OrderBy(x => x.u.FirstName)
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .Select(x => new ActivityParticipantDto
                {
                    StudentId = x.sa.StudentId,
                    StudentFullName = x.u.FirstName+' '+x.u.MiddleName+' '+x.u.LastName,
                    ManagementName = x.m.NameEn,
                    ManagementNameAr = x.m.NameAr,
                    
                    IsAttended = x.sa.AttendedAt != null,
                    NationalId=x.u.NationalId,
                    TargetAudienceName = _context.TargetAudiences
                        .Where(ta =>ta.Id == x.u.TargetaudienceId )
                        .Select(ta => ta.NameEn)
                        .FirstOrDefault() ?? string.Empty,

                    TargetAudienceNameAR = _context.TargetAudiences
                        .Where(ta =>ta.Id == x.u.TargetaudienceId )
                        .Select(ta => ta.NameAr)
                        .FirstOrDefault() ?? string.Empty,

                    Gender = (Gender)x.u.Gender,

                    HasEvaluated = _context.ActivityEvaluations
                        .Any(e =>
                            e.ActivityId == filter.ActivityId &&
                            e.StudentId == x.sa.StudentId),



                    StudentAverageRate = _context.ActivityEvaluations
                        .Where(e =>
                            e.ActivityId == filter.ActivityId &&
                            e.StudentId == x.sa.StudentId)
                        .Select(e => (double?)e.Value)
                        .Average()
                })
                .ToListAsync();

            return new PagedResult<ActivityParticipantDto>(items,totalCount,filter.PageNumber,filter.PageSize, filter.PageSize == 0 ? 1 : (int)Math.Ceiling(totalCount / (double)filter.PageSize));
        }

        // =========================
        // Modal (View Evaluation)
        // =========================
        public async Task<StudentEvaluationModalDto?> GetStudentEvaluationAsync(
            int activityId,
            int studentId)
        {
            var evaluation =
                from e in _context.ActivityEvaluations
                join c in _context.EvaluationCriteria
                    on e.EvaluationCriteriaId equals c.Id
                join u in _context.Users
                    on e.StudentId equals u.Id
                where e.ActivityId == activityId &&
                      e.StudentId == studentId
                select new { e, c, u };

            var list = await evaluation.ToListAsync();

            if (!list.Any())
                return null;

            return new StudentEvaluationModalDto
            {
                StudentName = list.First().u.FirstName +' '+ list.First().u.MiddleName+' '+ list.First().u.LastName,
                Criteria = list.Select(x => new StudentEvaluationCriteriaDto
                {
                    CriteriaName = x.c.NameEn,
                    Score = x.e.Value
                }).ToList()
            };
        }

        // =========================
        // Certificate Check
        // =========================
        //private async Task<bool>
        //    CanViewCertificateAsync(int activityId, int studentId)
        //{
        //    var attended = await _context.StudentActivities
        //        .AnyAsync(sa =>
        //            sa.ActivityId == activityId &&
        //            sa.StudentId == studentId &&
        //            sa.AttendedAt != null);

        //    var evaluated = await _context.ActivityEvaluations
        //        .AnyAsync(e =>
        //            e.ActivityId == activityId &&
        //            e.StudentId == studentId);

        //    return attended && evaluated;
        //}
    }


}
