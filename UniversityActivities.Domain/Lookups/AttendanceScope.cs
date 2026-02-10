using UniversityActivities.Domain.Entities;

public class AttendanceScope : BaseEntity
{
    [Required, MaxLength(150)]
    public string NameAr { get; set; }

    [Required, MaxLength(150)]
    public string NameEn { get; set; }

    public ICollection<Activity> Activities { get; set; }
    = new List<Activity>();
    public ICollection<Club> Clubs { get; set; }
       = new List<Club>();
}