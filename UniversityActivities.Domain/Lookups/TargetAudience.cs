using UniversityActivities.Domain.Entities;

public class TargetAudience : BaseEntity
{
    [Required, MaxLength(150)]
    public string NameAr { get; set; }

    [Required, MaxLength(150)]
    public string NameEn { get; set; }

    public ICollection<ActivityTargetAudience> ActivityTargetAudiences { get; set; }
        = new List<ActivityTargetAudience>();

}