using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using UniversityActivities.Application.Interfaces.Repositories.Roles;
using UniversityActivities.Domain.Enums;

namespace UniversityActivities.Infrastructure.Persistence.Repositories.Roles
{
    public class UserAccessQueryRepository : IUserAccessQueryRepository
    {
        private readonly AppDbContext _context;

        public UserAccessQueryRepository(AppDbContext context)
        {
            _context = context;
        }

        // =========================
        // System Roles
        // =========================

        public async Task<bool> IsSuperAdminAsync(int userId)
        {
            return await _context.UserRoles
                .AnyAsync(ur =>
                    ur.UserId == userId &&
                    _context.Roles.Any(r =>
                        r.Id == ur.RoleId &&
                        r.Name == "SuperAdmin"));
        }

        public async Task<bool> IsManagementSupervisorAsync(
            int userId,
            int managementId)
        {
            return await _context.UserRoles
                .AnyAsync(ur =>
                    ur.UserId == userId &&
                    _context.Roles.Any(r =>
                        r.Id == ur.RoleId &&
                        r.Name == "ManagementSupervisor") &&
                    _context.ManagementSupervisors.Any(ms =>
                        ms.UserId == userId &&
                        ms.ManagementId == managementId));
        }

        // =========================
        // Activity Role
        // =========================

        public async Task<ActivityRoles?> GetActivityRoleAsync(
            int userId,
            int activityId)
        {
            return await _context.ActivityUsers
                .Where(x =>
                    x.UserId == userId &&
                    x.ActivityId == activityId)
                .Select(x => (ActivityRoles?)x.ActivityRoleId)
                .FirstOrDefaultAsync();
        }

        public async Task<int> GetActivityManagementIdAsync(int activityId)
        {
            return await _context.Activities
                .Where(a => a.Id == activityId)
                .Select(a => a.ManagementId)
                .SingleAsync();
        }
    }
}
