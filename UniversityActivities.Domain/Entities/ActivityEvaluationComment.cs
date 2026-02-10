using UniversityActivities.Domain.Entities;

public class ActivityEvaluationComment : AuditableEntity
{
    [Required]
    public int ActivityId { get; set; }
    public Activity Activity { get; set; } = null!;

    [Required]
    public int StudentId { get; set; }

    [Required]
    [MaxLength(2000)]
    public string Comment { get; set; } = null!;
}
