using UniversityActivities.Infrastructure.Persistence;

namespace UniversityActivities.Infrastructure.Seed;

public static class ClubRolePermissionSeed
{
    public static async Task SeedAsync(AppDbContext context)
    {
        if (context.ClubRolePermissions.Any())
            return;

        var supervisorRole = context.ClubRoles.First(r => r.NameEn == "Club Supervisor");
        var memberRole = context.ClubRoles.First(r => r.NameEn == "Member");

        var manageClub = context.Permissions.First(p => p.Code == "MANAGE_CLUB");
        var addMember = context.Permissions.First(p => p.Code == "ADD_CLUB_MEMBER");
        var removeMember = context.Permissions.First(p => p.Code == "REMOVE_CLUB_MEMBER");

        context.ClubRolePermissions.AddRange(
            new ClubRolePermission
            {
                ClubRoleId = supervisorRole.Id,
                PermissionId = manageClub.Id
            },
            new ClubRolePermission
            {
                ClubRoleId = supervisorRole.Id,
                PermissionId = addMember.Id
            },
            new ClubRolePermission
            {
                ClubRoleId = supervisorRole.Id,
                PermissionId = removeMember.Id
            }
        );

        await context.SaveChangesAsync();
    }
}
