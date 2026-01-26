using UniversityActivities.Infrastructure.Persistence;

namespace UniversityActivities.Infrastructure.Seed;

public static class ActivityRolePermissionSeed
{
    public static async Task SeedAsync(AppDbContext context)
    {
        if (context.ActivityRolePermissions.Any())
            return;

        var coordinatorRole = context.ActivityRoles.First(r => r.NameEn == "Activity Coordinator");
        var approverRole = context.ActivityRoles.First(r => r.NameEn == "Activity Approver");

        var create = context.Permissions.First(p => p.Code == "CREATE_ACTIVITY");
        var update = context.Permissions.First(p => p.Code == "UPDATE_ACTIVITY");
        var approve = context.Permissions.First(p => p.Code == "APPROVE_ACTIVITY");
        var markAttendance = context.Permissions.First(p => p.Code == "MARK_ATTENDANCE");

        context.ActivityRolePermissions.AddRange(
            new ActivityRolePermission
            {
                ActivityRoleId = coordinatorRole.Id,
                PermissionId = create.Id
            },
            new ActivityRolePermission
            {
                ActivityRoleId = coordinatorRole.Id,
                PermissionId = update.Id
            },
            new ActivityRolePermission
            {
                ActivityRoleId = coordinatorRole.Id,
                PermissionId = markAttendance.Id
            },
            new ActivityRolePermission
            {
                ActivityRoleId = approverRole.Id,
                PermissionId = approve.Id
            }
        );

        await context.SaveChangesAsync();
    }
}
