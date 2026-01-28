using UniversityActivities.Domain.Lookups;
using UniversityActivities.Infrastructure.Persistence;

namespace UniversityActivities.Infrastructure.Seed;

public static class LookupSeed
{
    public static async Task SeedAsync(AppDbContext context)
    {
        if (!context.ManagementTypes.Any())
        {
            context.ManagementTypes.AddRange(
                new ManagementType { NameAr = "جامعة", NameEn = "University" },
                new ManagementType { NameAr = "عمادة", NameEn = "Deanship" },
                new ManagementType { NameAr = "كلية", NameEn = "College" },
                new ManagementType { NameAr = "نادي", NameEn = "Club" },
                new ManagementType { NameAr = "ادراة عامة", NameEn = "Public Management" },
                new ManagementType { NameAr = "اخري", NameEn = "Other" }
            );
        }

        if (!context.ActivityStatuses.Any())
        {
            context.ActivityStatuses.AddRange(
                new ActivityStatus { NameAr = "قيد التنفيذ", NameEn = "In progress" },
                new ActivityStatus { NameAr = "مكتمل", NameEn = "Completed" },
                new ActivityStatus { NameAr = "قريباً", NameEn = "Soon" }
            );
        }

        if (!context.EvaluationCriteria.Any())
        {
            context.EvaluationCriteria.AddRange(
                new EvaluationCriteria { NameAr = "جوده التنظيم", NameEn = "Organization" },
                new EvaluationCriteria { NameAr = "جودة المحتوى", NameEn = "Content" },
                new EvaluationCriteria { NameAr = "معدل الاستفادة من النشاط", NameEn = "Benefits" },
                new EvaluationCriteria { NameAr = "ملائمة طريقة العرض", NameEn = "Projection" },
                new EvaluationCriteria { NameAr = "منابة التوقيت", NameEn = "Timing" }
            );
        }

        await context.SaveChangesAsync();
    }
}
