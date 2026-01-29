public class ActivityEvaluation : AuditableEntity
{
    [Required]
    public int ActivityId { get; set; }

    [Required]
    public int StudentId { get; set; }
    [Required]
    public int EvaluationCriteriaId { get; set; }

    [Required]
    [Range(1, 5)]
    public int Value { get; set; }
}