public class ClubRolePermission : BaseEntity
{
    [Required]
    public int ClubRoleId { get; set; }

    [Required]
    public int PermissionId { get; set; }
}