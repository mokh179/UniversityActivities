public class Permission : BaseEntity
{
    [Required, MaxLength(150)]
    public string NameAr { get; set; }

    [Required, MaxLength(150)]
    public string NameEn { get; set; }

    [Required, MaxLength(200)]
    public string Code { get; set; }
    // Ex: CREATE_ACTIVITY, MARK_ATTENDANCE
}