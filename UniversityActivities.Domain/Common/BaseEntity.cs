using System;
using System.ComponentModel.DataAnnotations;

namespace UniversityActivities.Domain.Common;

public abstract class BaseEntity
{
    [Key]
    public int Id { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime? DeletedAt { get; set; }
}