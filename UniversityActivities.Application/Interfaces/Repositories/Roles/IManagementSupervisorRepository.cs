using System;
using System.Collections.Generic;
using System.Text;

namespace UniversityActivities.Application.Interfaces.Repositories.Roles
{
    public interface IManagementSupervisorRepository
    {
        Task<bool> ExistsAsync(int userId, int managementId);
        Task AddAsync(ManagementSupervisors entity);
    }
}
