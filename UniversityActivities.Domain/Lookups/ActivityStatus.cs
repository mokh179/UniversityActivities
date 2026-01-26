
public class ActivityStatus : BaseEntity
{
    [Required, MaxLength(100)]
    public string NameAr { get; set; }

    [Required, MaxLength(100)]
    public string NameEn { get; set; }
}