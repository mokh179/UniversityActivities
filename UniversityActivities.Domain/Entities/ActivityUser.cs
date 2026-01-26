

public class ActivityUser : AuditableEntity
{
    [Required]
    public int ActivityId { get; set; }

    [Required]
    public int UserId { get; set; }

    [Required]
    public int ActivityRoleId { get; set; }
}