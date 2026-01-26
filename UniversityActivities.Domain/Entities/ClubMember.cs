


public class ClubMember:AuditableEntity
{
    [Required]
    public int StudentClubId { get; set; }

    [Required]
    public int UserId { get; set; }

    [Required]
    public int ClubRoleId { get; set; }
}

