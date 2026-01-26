public class Activity : AuditableEntity
{
    // 🏷️ Basic Info
    [Required, MaxLength(250)]
    public string TitleAr { get; set; }

    [Required, MaxLength(250)]
    public string TitleEn { get; set; }

    [MaxLength(500)]
    public string ImageUrl { get; set; }

    // Ownership
    [Required]
    public int ManagementId { get; set; }

    // نشاط ممكن يبقى تابع لنادي أو لا
    public int? StudentClubId { get; set; }

    //  Lookups
    [Required]
    public int ActivityStatusId { get; set; }

    [Required]
    public int ActivityTypeId { get; set; }

    [Required]
    public int TargetAudienceId { get; set; }

    [Required]
    public int AttendanceScopeId { get; set; }

    [Required]
    public int AttendanceModeId { get; set; }

    //  Location / Online
    [MaxLength(300)]
    public string LocationAr { get; set; }

    [MaxLength(300)]
    public string LocationEn { get; set; }

    [MaxLength(500)]
    public string OnlineLink { get; set; }

    // ⏱️ Time
    [Required]
    public DateTime StartDateTime { get; set; }

    [Required]
    public DateTime EndDateTime { get; set; }

    [Required]
    public bool Published { get; set; } = false;
}