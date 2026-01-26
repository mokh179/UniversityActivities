public class StudentClub : AuditableEntity
{
    [Required, MaxLength(200)]
    public string NameAr { get; set; }

    [Required, MaxLength(200)]
    public string NameEn { get; set; }

    [Required, MaxLength(1000)]
    public string DescriptionAr { get; set; }

    [Required, MaxLength(1000)]
    public string DescriptionEn { get; set; }

    [MaxLength(500)]
    public string ImageUrl { get; set; }

    [Required]
    public int ManagementId { get; set; }

    [Required]
    public int ClubDomainId { get; set; }

    [Required]
    public int AttendanceScopeId { get; set; }

    [Required]
    public int MainSupervisorId { get; set; }

    [Required]
    public bool Published { get; set; } = false;
}