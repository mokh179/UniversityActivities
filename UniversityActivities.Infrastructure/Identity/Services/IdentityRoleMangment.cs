using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using UniversityActivities.Application.AuthorizationModule.Services.Interfaces;

namespace UniversityActivities.Infrastructure.Identity.Services
{
    public class IdentityRoleMangment : IIdentityRoleMangment
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public IdentityRoleMangment(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task EnsureUserInRoleAsync(int userId, string roleName)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
                throw new Exception("User not found.");

            // Ensure role exists
            if (!await _roleManager.RoleExistsAsync(roleName))
                throw new Exception($"Role '{roleName}' does not exist.");

            // Assign role if not already assigned
            if (!await _userManager.IsInRoleAsync(user, roleName))
                await _userManager.AddToRoleAsync(user, roleName);
        }
    }
}
