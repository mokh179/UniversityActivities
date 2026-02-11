using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using UniversityActivities.Application.AuthorizationModule.Models;
using UniversityActivities.Application.DTOs.Activities;
using UniversityActivities.Infrastructure.Identity;

namespace UniversityActivities.Infrastructure.Persistence.Repositories.Activities.Admin
{
    public class ActivityAssignmentRepository:IActivityAssignmentRepository
    {
        private readonly AppDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole<int>> _roleManager;

        public ActivityAssignmentRepository(AppDbContext context, RoleManager<IdentityRole<int>> roleManager,
                        UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task ReplaceAsync(
        int activityId,
        List<ActivityAssignmentDto> assignments)
        {
            try
            {
                //  Add new assignments
                if (assignments == null || assignments.Count == 0)
                    return;
                foreach (var assignment in assignments)
                {
                    if (assignment.Isnew.Value)
                    {
                        var user = await _userManager.FindByIdAsync(assignment.UserId.ToString());
                        if (user == null)
                            throw new Exception($"User with ID {assignment.UserId} not found.");
                        string roleName = assignment.ActivityRoleId switch { 1 => "Supervisor", 2 => "Coordinator", 3 => "Viewer", _ => "employee" };
                        if (!await _userManager.IsInRoleAsync(user, roleName))
                            await _userManager.AddToRoleAsync(user, roleName);

                    }
                }
                //  Remove existing assignments
                await _context.ActivityUsers.AsNoTracking()
                    .Where(x => x.ActivityId == activityId)
                    .ExecuteDeleteAsync();
                var entities = assignments
               .GroupBy(x => new { x.UserId, x.ActivityRoleId })
               .Select(g => g.First())
               .Select(x => new ActivityUser
               {
                   ActivityId = activityId,
                   UserId = x.UserId,
                   ActivityRoleId = x.ActivityRoleId
               })
               .ToList();

                await _context.ActivityUsers.AddRangeAsync(entities);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

    }
}
