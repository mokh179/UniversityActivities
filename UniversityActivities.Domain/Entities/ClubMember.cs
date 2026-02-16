


using UniversityActivities.Domain.Entities;
using UniversityActivities.Domain.Enums;

public class ClubMember:AuditableEntity
{
    [Required]
    public int ClubId { get; set; }

    [Required]
    public int UserId { get; set; }

    [Required]
    public ClubRole Role { get; set; }
    public Club Club { get; set; }= null!;
}



