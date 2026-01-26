
    public class UserManagementRole : AuditableEntity
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public int ManagementId { get; set; }

        [Required]
        public int ManagementRoleId { get; set; }
    }

