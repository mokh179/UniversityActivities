using UniversityActivities.Domain.Entities;

public class ActivityType : BaseEntity
{
    [Required, MaxLength(150)]
    public string NameAr { get; set; }

    [Required, MaxLength(150)]
    public string NameEn { get; set; }

    public ICollection<Activity> Activities { get; set; }
    = new List<Activity>();
}