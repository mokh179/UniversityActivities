using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using UniversityActivities.Domain.Entities;

namespace UniversityActivities.Domain.Lookups;

public class TargetAudience : BaseEntity
{
    [Required, MaxLength(150)]
    public string NameAr { get; set; } = string.Empty;

    [Required, MaxLength(150)]
    public string NameEn { get; set; } = string.Empty;

    public ICollection<ActivityTargetAudience> ActivityTargetAudiences { get; set; } = new List<ActivityTargetAudience>();
}