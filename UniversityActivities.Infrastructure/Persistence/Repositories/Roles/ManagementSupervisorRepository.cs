using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using UniversityActivities.Application.Interfaces.Repositories.Roles;

namespace UniversityActivities.Infrastructure.Persistence.Repositories.Roles
{
    public class ManagementSupervisorRepository:IManagementSupervisorRepository
    {
        private readonly AppDbContext _context;

        public ManagementSupervisorRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> ExistsAsync(int userId, int managementId)
        {
            return await _context.ManagementSupervisors
                .AnyAsync(x =>
                    x.UserId == userId &&
                    x.ManagementId == managementId);
        }
        public async Task AddAsync(ManagementSupervisors entity)
        {
            await _context.ManagementSupervisors.AddAsync(entity);
        }
    }
}
