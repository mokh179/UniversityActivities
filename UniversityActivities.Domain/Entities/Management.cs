
public class Management :AuditableEntity
{
    [Required, MaxLength(200)]
    public string NameAr { get; set; }

    [Required, MaxLength(200)]
    public string NameEn { get; set; }

    [Required]
    public int ManagementTypeId { get; set; }
}