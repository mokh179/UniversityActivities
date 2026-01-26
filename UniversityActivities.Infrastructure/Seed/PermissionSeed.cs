using UniversityActivities.Domain.RBAC;
using UniversityActivities.Infrastructure.Persistence;

namespace UniversityActivities.Infrastructure.Seed;

public static class PermissionSeed
{
    public static async Task SeedAsync(AppDbContext context)
    {
        if (context.Permissions.Any())
            return;

        var permissions = new List<Permission>
        {
            // =========================
            // Activities
            // =========================
            new Permission
            {
                Code = "CREATE_ACTIVITY",
                NameAr = "إنشاء نشاط",
                NameEn = "Create Activity"
            },
            new Permission
            {
                Code = "UPDATE_ACTIVITY",
                NameAr = "تعديل نشاط",
                NameEn = "Update Activity"
            },
            new Permission
            {
                Code = "DELETE_ACTIVITY",
                NameAr = "حذف نشاط",
                NameEn = "Delete Activity"
            },
            new Permission
            {
                Code = "APPROVE_ACTIVITY",
                NameAr = "اعتماد نشاط",
                NameEn = "Approve Activity"
            },

            // =========================
            // Clubs
            // =========================
            new Permission
            {
                Code = "MANAGE_CLUB",
                NameAr = "إدارة نادي طلابي",
                NameEn = "Manage Club"
            },
            new Permission
            {
                Code = "ADD_CLUB_MEMBER",
                NameAr = "إضافة عضو للنادي",
                NameEn = "Add Club Member"
            },
            new Permission
            {
                Code = "REMOVE_CLUB_MEMBER",
                NameAr = "إزالة عضو من النادي",
                NameEn = "Remove Club Member"
            },

            // =========================
            // Participation
            // =========================
            new Permission
            {
                Code = "REGISTER_ACTIVITY",
                NameAr = "التسجيل في نشاط",
                NameEn = "Register Activity"
            },
            new Permission
            {
                Code = "MARK_ATTENDANCE",
                NameAr = "تسجيل الحضور",
                NameEn = "Mark Attendance"
            },

            // =========================
            // Evaluation
            // =========================
            new Permission
            {
                Code = "EVALUATE_ACTIVITY",
                NameAr = "تقييم نشاط",
                NameEn = "Evaluate Activity"
            },
            new Permission
            {
                Code = "VIEW_EVALUATION_REPORTS",
                NameAr = "عرض تقارير التقييم",
                NameEn = "View Evaluation Reports"
            },

            // =========================
            // Administration
            // =========================
            new Permission
            {
                Code = "VIEW_REPORTS",
                NameAr = "عرض التقارير",
                NameEn = "View Reports"
            },
            new Permission
            {
                Code = "MANAGE_ROLES",
                NameAr = "إدارة الأدوار والصلاحيات",
                NameEn = "Manage Roles & Permissions"
            }
        };

        context.Permissions.AddRange(permissions);
        await context.SaveChangesAsync();
    }
}
