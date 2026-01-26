using Microsoft.AspNetCore.Identity;
using UniversityActivities.Infrastructure.Persistence;

namespace UniversityActivities.Infrastructure.Identity;

public static class IdentitySeed
{
    public static async Task SeedAsync(
        UserManager<ApplicationUser> userManager,
        AppDbContext context)
    {
        const string superAdminEmail = "superadmin@ub.edu.sa";
        const string superAdminPassword = "Admin@123";

        var user = await userManager.FindByEmailAsync(superAdminEmail);

        if (user == null)
        {
            user = new ApplicationUser
            {
                UserName = superAdminEmail,
                Email = superAdminEmail,
                EmailConfirmed = true
            };

            var result = await userManager.CreateAsync(user, superAdminPassword);
            if (!result.Succeeded)
                throw new Exception("Failed to create Super Admin user");
        }

        // =========================
        // Attach Management Role
        // =========================
        var adminRole = context.ManagementRoles.FirstOrDefault(r => r.NameEn == "Management Admin");
        if (adminRole == null)
            throw new Exception("Management Admin role not found");

        var hasRole = context.UserManagementRoles.Any(x =>
            x.UserId == user.Id &&
            x.ManagementRoleId == adminRole.Id);

        if (!hasRole)
        {
            context.UserManagementRoles.Add(new UserManagementRole
            {
                UserId = user.Id,
                ManagementRoleId = adminRole.Id,
                ManagementId = context.Managements.First().Id // الجامعة
            });

            await context.SaveChangesAsync();
        }
    }
}
