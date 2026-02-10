using UniversityActivities.Domain.Entities;

public class ClubDomain : BaseEntity
{
    [Required, MaxLength(150)]
    public string NameAr { get; set; }

    [Required, MaxLength(150)]
    public string NameEn { get; set; }
    public ICollection<Club> Clubs { get; set; }
       = new List<Club>();
}