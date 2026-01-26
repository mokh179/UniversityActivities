public class ActivityEvaluation : AuditableEntity
{
    [Required]
    public int ActivityId { get; set; }

    [Required]
    public int StudentId { get; set; }
}