using Microsoft.AspNetCore.Identity;
using UniversityActivities.Infrastructure.Identity;
using UniversityActivities.Infrastructure.Persistence;
using UniversityActivities.Infrastructure.Seed;

public static class SeedExtensions
{
    public static async Task SeedDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<int>>>();

        await LookupSeed.SeedAsync(context);

        await RolesSeed.SeedAsync(context, roleManager);
        await IdentitySeed.SeedAsync(userManager,roleManager, context);
    }
}
