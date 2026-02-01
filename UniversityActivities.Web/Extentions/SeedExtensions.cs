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

        await LookupSeed.SeedAsync(context);

        await IdentitySeed.SeedAsync(userManager, context);
    }
}
