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

        #region Students
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
        #endregion

        #region coordinator
        var coordinator = new ApplicationUser
        {
            UserName = "coordinatorMK17",
            FirstName = "Mohammed",
            MiddleName = "Khaled",
            LastName = "Ahmed",
            Gender = Application.AuthorizationModule.Models.AuthModels.Gender.Male,
            ManagementId = 1,
            NationalId = "2628",
            TargetaudienceId = 1,
            Email = "coordinator@ub.edu.sa",
            EmailConfirmed = true
        };
        if (await userManager.FindByEmailAsync(coordinator.Email) == null)
        {
            var result = await userManager.CreateAsync(coordinator, superAdminPassword);
            if (!result.Succeeded)
                throw new Exception($"Failed to create user {coordinator.Email}");
            var Role = await userManager.AddToRoleAsync(coordinator, SystemRoles.ManagementCoordinator);
        }
        #endregion

        #region supervisor
        var supervisor = new ApplicationUser
        {
            UserName = "supervisorMK17",
            FirstName = "Mohammed",
            MiddleName = "Khaled",
            LastName = "Ahmed",
            Gender = Application.AuthorizationModule.Models.AuthModels.Gender.Male,
            ManagementId = 1,
            NationalId = "2628",
            TargetaudienceId = 1,
            Email = "supervisor@ub.edu.sa",
            EmailConfirmed = true
        };
        if (await userManager.FindByEmailAsync(supervisor.Email) == null)
        {
            var result = await userManager.CreateAsync(supervisor, superAdminPassword);
            if (!result.Succeeded)
                throw new Exception($"Failed to create user {supervisor.Email}");
            var Role = await userManager.AddToRoleAsync(supervisor, SystemRoles.ManagementSupervisor);
        }
        #endregion

        #region Viewer
        var viewer = new ApplicationUser
        {
            UserName = "viewerMK17",
            FirstName = "Mohammed",
            MiddleName = "Khaled",
            LastName = "Ahmed",
            Gender = Application.AuthorizationModule.Models.AuthModels.Gender.Male,
            ManagementId = 1,
            NationalId = "2628",
            TargetaudienceId = 1,
            Email = "viewer@ub.edu.sa",
            EmailConfirmed = true
        };
        if (await userManager.FindByEmailAsync(viewer.Email) == null)
        {
            var result = await userManager.CreateAsync(viewer, superAdminPassword);
            if (!result.Succeeded)
                throw new Exception($"Failed to create user {viewer.Email}");
            var Role = await userManager.AddToRoleAsync(viewer, SystemRoles.ManagementViewer);
        }
        #endregion


        #region Super Admin
        var superadmin = new ApplicationUser
        {
            UserName = "superadminMK17",
            FirstName = "Mohammed",
            MiddleName = "Khaled",
            LastName = "Ahmed",
            Gender = Application.AuthorizationModule.Models.AuthModels.Gender.Male,
            ManagementId = 1,
            NationalId = "2628",
            TargetaudienceId = 1,
            Email = "superadmin@ub.edu.sa",
            EmailConfirmed = true
        };
        if (await userManager.FindByEmailAsync(superadmin.Email) == null)
        {
            var result = await userManager.CreateAsync(superadmin, superAdminPassword);
            if (!result.Succeeded)
                throw new Exception($"Failed to create user {viewer.Email}");
            var Role = await userManager.AddToRoleAsync(superadmin, SystemRoles.SuperAdmin);
        } 
        #endregion
        #region Employee
        var Employee = new ApplicationUser
        {
            UserName = "EmployeeMK17",
            FirstName = "Mohammed",
            MiddleName = "Khaled",
            LastName = "Ahmed",
            Gender = Application.AuthorizationModule.Models.AuthModels.Gender.Male,
            ManagementId = 1,
            NationalId = "2628",
            TargetaudienceId = 1,
            Email = "Employee@ub.edu.sa",
            EmailConfirmed = true
        };
        if (await userManager.FindByEmailAsync(Employee.Email) == null)
        {
            var result = await userManager.CreateAsync(Employee, superAdminPassword);
            if (!result.Succeeded)
                throw new Exception($"Failed to create user {viewer.Email}");
            var Role = await userManager.AddToRoleAsync(Employee, SystemRoles.Employee);
        } 
        #endregion





        await context.SaveChangesAsync();

    }
}
