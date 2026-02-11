using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using UniversityActivities.Infrastructure.Persistence;

namespace UniversityActivities.Infrastructure.Seed
{
    public static class RolesSeed
    {
      
        public static async Task SeedAsync(AppDbContext context , RoleManager<IdentityRole<int>> roleManager)
        {
               List<string> Roles = new()
                    {
                        "SuperAdmin",
                        "Supervisor",
                        "Student",
                        "Coordinator",
                        "Viewer",
                        "Employee"
                    };


            foreach (var role in Roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole<int>(role));
                }
            }
            await context.SaveChangesAsync();
        }
    }
}
