using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using UniversityActivities.Application.AuthorizationModule.Models;
using UniversityActivities.Application.AuthorizationModule.Models.AuthModels;
using UniversityActivities.Application.AuthorizationModule.Services.Interfaces;
using System.Security.Claims;
namespace UniversityActivities.Infrastructure.Identity.Services
{
    public class IdentityMangment : IIdentityMangment
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public IdentityMangment(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<int> CreateUserAsync(RegisterDto registerDto)
        {
            var isExistBefore = _userManager.FindByNameAsync(registerDto.UserName);
            if (isExistBefore == null)
            {
                var user = new ApplicationUser
                {
                    UserName = registerDto.UserName,
                    Email = registerDto.Email,
                    ManagementId = registerDto.ManagmentId,
                    Gender = registerDto.Gender
                };
                var result =await _userManager.CreateAsync(user, registerDto.Password);

                if (!result.Succeeded)
                    throw new Exception(string.Join(", ",
                        result.Errors.Select(e => e.Description)));

                await _userManager.AddToRoleAsync(user, SystemRoles.Student);

                return user.Id;
            }
            throw new Exception("User already exists.");
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

        public async Task<(int UserId, string UserName, List<Claim> Claims)>GenerateClaimsAsync(int userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString())
                ?? throw new Exception("User not found");

            var roles = await _userManager.GetRolesAsync(user);

            var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                        new Claim(ClaimTypes.Name, user.UserName!),
                        new Claim("management_id", user.ManagementId.ToString()),
                        new Claim("gender", user.Gender.ToString()),
                        new Claim("TargetAudience", user.Gender.ToString()),
                    };

            foreach (var role in roles)
                claims.Add(new Claim(ClaimTypes.Role, role));

            return (userId, user.UserName!, claims);
        }

        public async Task<(int UserId, string UserName)> LoginAsync(LoginDto loginDto)
        {
            var user = await _userManager.FindByNameAsync(loginDto.UserNameOrEmail) ?? await _userManager.FindByEmailAsync(loginDto.UserNameOrEmail);

            if (user == null)
                throw new Exception("Invalid username/email or password.");

            var valid = await _userManager.CheckPasswordAsync(user, loginDto.Password);
            if (!valid)
                throw new Exception("Invalid username/email or password.");
            return (user.Id, user.UserName!);
        }
    }
}
