public class ActivityEvaluationComment : AuditableEntity
{
    [Required]
    public int ActivityId { get; set; }

    [Required]
    public int StudentId { get; set; }

    [Required]
    [MaxLength(2000)]
    public string Comment { get; set; } = null!;
}
