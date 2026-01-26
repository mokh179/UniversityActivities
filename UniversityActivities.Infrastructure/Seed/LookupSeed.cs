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
                new ManagementType { NameAr = "كلية", NameEn = "College" }
            );
        }

        if (!context.ActivityStatuses.Any())
        {
            context.ActivityStatuses.AddRange(
                new ActivityStatus { NameAr = "مسودة", NameEn = "Draft" },
                new ActivityStatus { NameAr = "بانتظار الاعتماد", NameEn = "Pending Approval" },
                new ActivityStatus { NameAr = "معتمد", NameEn = "Approved" },
                new ActivityStatus { NameAr = "مرفوض", NameEn = "Rejected" }
            );
        }

        if (!context.EvaluationCriteria.Any())
        {
            context.EvaluationCriteria.AddRange(
                new EvaluationCriteria { NameAr = "التنظيم", NameEn = "Organization" },
                new EvaluationCriteria { NameAr = "المحتوى", NameEn = "Content" },
                new EvaluationCriteria { NameAr = "التوقيت", NameEn = "Timing" }
            );
        }

        await context.SaveChangesAsync();
    }
}
