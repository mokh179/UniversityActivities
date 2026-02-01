using System;
using System.Collections.Generic;
using System.Text;
using UniversityActivities.Domain.Enums;

namespace UniversityActivities.Application.Interfaces.Repositories.Roles
{
    public interface IUserAccessQueryRepository
    {
        Task<bool> IsSuperAdminAsync(int userId);

        Task<bool> IsManagementSupervisorAsync(int userId, int managementId);

        Task<ActivityRoles?> GetActivityRoleAsync(int userId, int activityId);
        Task<int> GetActivityManagementIdAsync(int activityId);

    }
}
