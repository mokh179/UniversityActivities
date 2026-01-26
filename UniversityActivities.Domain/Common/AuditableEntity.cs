
namespace UniversityActivities.Domain.Common;

public abstract class AuditableEntity:BaseEntity
{
    [Required]
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}