public class EvaluationScore : BaseEntity
{
    [Required]
    public int ActivityEvaluationId { get; set; }

    [Required]
    public int EvaluationCriteriaId { get; set; }

    [Required]
    [Range(1, 5)]
    public int Score { get; set; }
}
