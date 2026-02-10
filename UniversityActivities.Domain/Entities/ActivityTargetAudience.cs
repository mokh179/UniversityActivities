
namespace UniversityActivities.Domain.Entities;

public class ActivityTargetAudience : AuditableEntity
{
    public int ActivityId { get; set; }
    public int TargetAudienceId { get; set; }

    public Activity Activity { get; set; } = null!;
    public TargetAudience TargetAudience { get; set; } = null!;
}
