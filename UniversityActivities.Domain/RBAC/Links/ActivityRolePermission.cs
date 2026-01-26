public class ActivityRolePermission : BaseEntity
{
    [Required]
    public int ActivityRoleId { get; set; }

    [Required]
    public int PermissionId { get; set; }
}