using UniversityActivities.Domain.RBAC;
using UniversityActivities.Infrastructure.Persistence;

namespace UniversityActivities.Infrastructure.Seed;

public static class RoleSeed
{
    public static async Task SeedAsync(AppDbContext context)
    {
        // =========================
        // Management Roles
        // =========================
        if (!context.ManagementRoles.Any())
        {
            context.ManagementRoles.AddRange(
                new ManagementRole
                {
                    NameAr = "مشرف",
                    NameEn = "Supervisor"
                },
                new ManagementRole
                {
                    NameAr = "منسق",
                    NameEn = "Coordinator"
                },
                new ManagementRole
                {
                    NameAr = "مشاهد",
                    NameEn = "Viewer"
                },
                new ManagementRole
                {
                    NameAr = "معتمد",
                    NameEn = "Approver"
                }
            );
        }

        // =========================
        // Club Roles
        // =========================
        if (!context.ClubRoles.Any())
        {
            context.ClubRoles.AddRange(
                new ClubRole
                {
                    NameAr = "مشرف النادي",
                    NameEn = "Club Supervisor"
                },
                new ClubRole
                {
                    NameAr = "عضو",
                    NameEn = "Member"
                }
            );
        }

        // =========================
        // Activity Roles
        // =========================
        if (!context.ActivityRoles.Any())
        {
            context.ActivityRoles.AddRange(
                new ActivityRole
                {
                    NameAr = "منسق النشاط",
                    NameEn = "Activity Coordinator"
                },
                new ActivityRole
                {
                    NameAr = "معتمد النشاط",
                    NameEn = "Activity Approver"
                }
            );
        }

        await context.SaveChangesAsync();
    }
}
