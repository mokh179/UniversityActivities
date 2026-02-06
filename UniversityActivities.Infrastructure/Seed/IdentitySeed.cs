using Microsoft.AspNetCore.Identity;
using UniversityActivities.Application.AuthorizationModule.Models;
using UniversityActivities.Application.AuthorizationModule.Models.AuthModels;
using UniversityActivities.Infrastructure.Persistence;

namespace UniversityActivities.Infrastructure.Identity;

public static class IdentitySeed
{
    public static async Task SeedAsync(
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole<int>> roleManager,
        AppDbContext context)
    {


        var superAdminPassword="Admin@123";
        List<ApplicationUser> Users = new List<ApplicationUser>
        { 
          new ApplicationUser {
                UserName = "AdminMK17",
                FirstName = "Mohammed",
                MiddleName = "Khaled",
                LastName = "Ahmed",
                Gender = Application.AuthorizationModule.Models.AuthModels.Gender.Male,
                ManagementId = 1,
                NationalId = "2628",
                TargetaudienceId = 1,
                Email = "superadmin@ub.edu.sa",
                EmailConfirmed = true
            },
          new ApplicationUser {
                UserName = "Mokh25",
                FirstName = "Mohammed",
                MiddleName = "Ahmed",
                LastName = "Ahmed",
                Gender = Application.AuthorizationModule.Models.AuthModels.Gender.Male,
                ManagementId = 1,
                NationalId = "2628",
                TargetaudienceId = 1,
                Email = "Mokh25@ub.edu.sa",
                EmailConfirmed = true
            },new ApplicationUser {
                UserName = "mk47",
                FirstName = "Mohammed",
                MiddleName = "ALi",
                LastName = "Ahmed",
                Gender = Application.AuthorizationModule.Models.AuthModels.Gender.Male,
                ManagementId = 1,
                NationalId = "2628",
                TargetaudienceId = 1,
                Email = "mk47@ub.edu.sa",
                EmailConfirmed = true
            },
        }; 
        foreach (var user in Users)
        {
            var existingUser = await userManager.FindByEmailAsync(user.Email);
            if (existingUser == null)
            {
                var result = await userManager.CreateAsync(user, superAdminPassword);
                if (!result.Succeeded)
                    throw new Exception($"Failed to create user {user.Email}");
                var Role = await userManager.AddToRoleAsync(user, SystemRoles.Student);

            }
        }
     

       
        await context.SaveChangesAsync();

    }
}
