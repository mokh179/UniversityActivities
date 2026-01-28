using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using UniversityActivities.Application.Common.Models;
using UniversityActivities.Application.DTOs.Activities;
using UniversityActivities.Application.Interfaces.Repositories.Activies.AdminActivies;
using UniversityActivities.Domain.Entities;

namespace UniversityActivities.Infrastructure.Persistence.Repositories.Activities.Admin
{
    public class AdminActivityRepository : IAdminActivityRepository
    {
        private readonly AppDbContext _context;

        public AdminActivityRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<int> CreateAsync(Activity activity)
        {
            _context.Activities.Add(activity);
            await _context.SaveChangesAsync();
            return activity.Id;
        }

        public async Task UpdateAsync(Activity activity)
        {
            _context.Activities.Update(activity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int activityId)
        {
            var activity = await _context.Activities
                .FirstOrDefaultAsync(x => x.Id == activityId);

            if (activity == null)
                return;

            activity.IsDeleted = true;
            await _context.SaveChangesAsync();
        }

        public Task SetPublishStatusAsync(int activityId, bool isPublished)
        {
            throw new NotImplementedException();
        }

        public Task<PagedResult<ActivityAdminListItemDto>> GetAllAsync(ActivityAdminFilter filter, PagedRequest paging)
        {
            throw new NotImplementedException();
        }

        public Task<ActivityAdminDetailsDto?> GetDetailsAsync(int activityId)
        {
            throw new NotImplementedException();
        }
    }
}
