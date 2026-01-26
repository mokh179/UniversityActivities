public class EvaluationCriteria : BaseEntity
{
    [Required, MaxLength(150)]
    public string NameAr { get; set; }

    [Required, MaxLength(150)]
    public string NameEn { get; set; }
}