using System;
using System.Collections.Generic;
using System.Text;
using UniversityActivities.Application.DTOs.Admin;

namespace UniversityActivities.Application.UserCases.Admin
{
    public interface IAdminDashboardUseCase
    {
        Task<AdminDashboardStatsDto> ExecuteAsync(int managementId, bool isSuperAdmin);
    }
}
