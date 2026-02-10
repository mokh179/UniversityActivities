using System;
using System.Collections.Generic;
using System.Text;
using UniversityActivities.Application.DTOs.Admin;

namespace UniversityActivities.Application.Interfaces.Repositories.Admin
{
    public interface IDashboardQueryRepository
    {
        Task<AdminDashboardStatsDto> GetStatsAsync(int managementId);
        Task<AdminDashboardStatsDto> GetGlobalStatsAsync();
    }
}
