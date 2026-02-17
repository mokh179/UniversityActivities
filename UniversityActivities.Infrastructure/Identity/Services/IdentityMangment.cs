using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using UniversityActivities.Application.AuthorizationModule.Models;
using UniversityActivities.Application.AuthorizationModule.Models.AuthModels;
using UniversityActivities.Application.AuthorizationModule.Services.Interfaces;
using UniversityActivities.Application.Exceptions;
namespace UniversityActivities.Infrastructure.Identity.Services
{
    public class IdentityMangment : IIdentityMangment
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole<int>> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public IdentityMangment(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole<int>> roleManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        public async Task<int> CreateUserAsync(RegisterDto registerDto)
        {
            var isexisting = await _userManager.FindByEmailAsync(registerDto.UserName);
            if (isexisting == null)
            {
                try
                {
                    var user = new ApplicationUser
                    {
                        UserName = registerDto.UserName,
                        FirstName= registerDto.FirstName,
                        LastName= registerDto.LastName,
                        MiddleName= registerDto.LastName,
                        Email = registerDto.Email,
                        ManagementId = registerDto.ManagmentId,
                        Gender = registerDto.Gender,
                        TargetaudienceId = 1,
                        NationalId = registerDto.NationalId,
                    };
                    var result = await _userManager.CreateAsync(user, registerDto.Password);

                    if (!result.Succeeded)
                        throw new BusinessException(string.Join(", ",
                            result.Errors.Select(e => e.Description)));

                    await _userManager.AddToRoleAsync(user, SystemRoles.Student);
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return user.Id;
                }
                catch (Exception ex)
                {

                    new BusinessException("An error occured try again later.");
                }
            }
            throw new BusinessException("User already exists.");
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

        public  async Task<(int UserId, string UserName, List<Claim> Claims)>GenerateClaimsAsync(int userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString())
                ?? throw new Exception("User not found");

            var roles = await _userManager.GetRolesAsync(user);

            var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                        new Claim(ClaimTypes.Name, user.UserName!),
                        new Claim("Management_id", user.ManagementId.ToString()),
                        new Claim("Gender", user.Gender.ToString()),
                        new Claim("TargetAudience", user.Gender.ToString()),
                    };

            foreach (var role in roles)
                claims.Add(new Claim(ClaimTypes.Role, role));

            return (userId, user.UserName!, claims);
        }


    
        public async Task<(int UserId, string UserName,bool isAdmin)> LoginAsync(LoginDto loginDto)
        {
            var user = await _userManager.FindByNameAsync(loginDto.UserNameOrEmail) ?? await _userManager.FindByEmailAsync(loginDto.UserNameOrEmail);

            if (user == null)
                throw new Exception("Invalid username/email or password.");

            var result = await _signInManager.PasswordSignInAsync(
                  user,
                  loginDto.Password,
                  isPersistent: false,
                  lockoutOnFailure: false
              );
            if (result==null)
                throw new Exception("Invalid username/email or password.");

            bool isAdmin = await _userManager.IsInRoleAsync(user, "Employee");
            return (user.Id, user.UserName!,isAdmin);
        }
        public async Task  LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
