
using UniversityActivities.Domain.Entities;

public class Management :AuditableEntity
{
    [Required, MaxLength(200)]
    public string NameAr { get; set; }

    [Required, MaxLength(200)]
    public string NameEn { get; set; }

    [Required]
    public int ManagementTypeId { get; set; }

    // Navigation
    public ICollection<Activity> Activities { get; set; } = new List<Activity>();
    public ICollection<Club> Clubs { get; set; } = new List<Club>();

}