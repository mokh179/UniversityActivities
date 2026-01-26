using UniversityActivities.Infrastructure.Persistence;

namespace UniversityActivities.Infrastructure.Seed;

public static class ManagementRolePermissionSeed
{
    public static async Task SeedAsync(AppDbContext context)
    {
        if (context.ManagementRolePermissions.Any())
            return;

        var adminRole = context.ManagementRoles.First(r => r.NameEn == "Management Admin");
        var approverRole = context.ManagementRoles.First(r => r.NameEn == "Approver");
        var viewerRole = context.ManagementRoles.First(r => r.NameEn == "Viewer");

        var permissions = context.Permissions.ToList();

        var adminPermissions = permissions.Select(p => new ManagementRolePermission
        {
            ManagementRoleId = adminRole.Id,
            PermissionId = p.Id
        });

        var approverPermissions = permissions
            .Where(p => p.Code.Contains("APPROVE") || p.Code.Contains("VIEW"))
            .Select(p => new ManagementRolePermission
            {
                ManagementRoleId = approverRole.Id,
                PermissionId = p.Id
            });

        var viewerPermissions = permissions
            .Where(p => p.Code.StartsWith("VIEW"))
            .Select(p => new ManagementRolePermission
            {
                ManagementRoleId = viewerRole.Id,
                PermissionId = p.Id
            });

        context.ManagementRolePermissions.AddRange(
            adminPermissions
                .Concat(approverPermissions)
                .Concat(viewerPermissions)
        );

        await context.SaveChangesAsync();
    }
}
