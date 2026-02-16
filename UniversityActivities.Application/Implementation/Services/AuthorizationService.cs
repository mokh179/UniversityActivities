using System;
using System.Collections.Generic;
using System.Text;
using UniversityActivities.Application.AuthorizationModule.Models;
using UniversityActivities.Application.AuthorizationModule.Services.Interfaces;
using UniversityActivities.Application.Interfaces.Repositories.Roles;
using UniversityActivities.Domain.Enums;

namespace UniversityActivities.Application.Implementation.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly IUserAccessQueryRepository _userAccessQueryRepository;

        public AuthorizationService(
            IUserAccessQueryRepository userAccessQueryRepository)
        {
            _userAccessQueryRepository = userAccessQueryRepository;
        }

        public async Task EnsureAuthorizedAsync(
    int userId,
    AuthorizationAction action,
    AuthorizationContext context)
        {
            // =========================
            // 1. Super Admin (Administrative only)
            // =========================
            if (await _userAccessQueryRepository.IsSuperAdminAsync(userId))
                return;



            // =========================
            // 2. Management Supervisor
            // =========================
            if (context.ActivityId.HasValue)
            {
                var mangmentId = await _userAccessQueryRepository.GetActivityManagementIdAsync(context.ActivityId.Value);
                if (await _userAccessQueryRepository.IsManagementSupervisorAsync(userId, mangmentId))
                    return;
            }

            // =========================
            // 3. Activity Role
            // =========================
            if (context.ActivityId.HasValue)
            {
                var role = await _userAccessQueryRepository
                    .GetActivityRoleAsync(userId, context.ActivityId.Value);

                if (role.HasValue && IsAllowed(role.Value, action))
                    return;
            }

            throw new UnauthorizedAccessException("Not authorized.");
        }


        private static bool IsAllowed(
    ActivityRoles role,
    AuthorizationAction action)
        {
            return role switch
            {
                ActivityRoles.Supervisor => true,

                ActivityRoles.Coordinator => action switch
                {
                    AuthorizationAction.ViewActivityStudents => true,
                    AuthorizationAction.ViewStudentStatus => true,
                    AuthorizationAction.ViewStudentCertificate => true,
                    AuthorizationAction.MarkStudentAttendance => true,
                    _ => false
                },

                ActivityRoles.Viewer => action switch
                {
                    AuthorizationAction.ViewActivityStudents => true,
                    AuthorizationAction.ViewStudentStatus => true,
                    _ => false
                },

                _ => false
            };
        }




    }
}
