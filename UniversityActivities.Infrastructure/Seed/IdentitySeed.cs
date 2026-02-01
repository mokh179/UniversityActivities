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
                FirstName="Mohammed",
                MiddleName= "Khaled",
                LastName="Ahmed",
                Gender=Application.AuthorizationModule.Models.AuthModels.Gender.Male,
                ManagementId=1,
                NationalId="2628",
                TargetaudienceId=1,
                Email = superAdminEmail,
                EmailConfirmed = true
            };

            var result = await userManager.CreateAsync(user, superAdminPassword);
            if (!result.Succeeded)
                throw new Exception("Failed to create Super Admin user");
        }

       
        await context.SaveChangesAsync();

    }
}
