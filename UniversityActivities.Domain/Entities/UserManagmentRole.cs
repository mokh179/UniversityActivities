
    public class ManagementSupervisors : AuditableEntity
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public int ManagementId { get; set; }

        
    }

