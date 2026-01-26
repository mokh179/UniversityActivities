
public class ManagementRolePermission : BaseEntity
{
    [Required]
    public int ManagementRoleId { get; set; }

    [Required]
    public int PermissionId { get; set; }
}