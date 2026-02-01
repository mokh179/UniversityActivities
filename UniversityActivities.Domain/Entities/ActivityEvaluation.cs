using UniversityActivities.Domain.Entities;

public class ActivityEvaluation : AuditableEntity
{
    [Required]
    public int ActivityId { get; set; }
    public Activity Activity { get; set; } = null!;

    [Required]
    public int StudentId { get; set; }
    [Required]
    public int EvaluationCriteriaId { get; set; }

    [Required]
    [Range(1, 5)]
    public int Value { get; set; }

}