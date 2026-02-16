using System;
using System.Collections.Generic;
using System.Text;
using UniversityActivities.Application.DTOs.Admin;
using UniversityActivities.Application.Interfaces.Repositories.Admin;
using UniversityActivities.Application.UserCases.Admin;

namespace UniversityActivities.Application.Implementation.ApplyUseCases.AdminDashboard
{
    public class AdminDashboardUseCase : IAdminDashboardUseCase
    {
        private readonly IDashboardQueryRepository _query;

        public AdminDashboardUseCase(IDashboardQueryRepository query)
        {
            _query = query;
        }

        public Task<AdminDashboardStatsDto> ExecuteAsync(
            int managementId,
            bool isSuperAdmin)
        {
            if (isSuperAdmin || managementId == 0)
            {
                return _query.GetGlobalStatsAsync();
            }

            // Management Supervisor → Management واحدة
            return _query.GetStatsAsync(managementId);
        }
    }
}
