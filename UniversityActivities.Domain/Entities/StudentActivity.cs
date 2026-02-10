
using UniversityActivities.Domain.Entities;

public class StudentActivity : AuditableEntity
{
    [Required]
    public int StudentId { get; set; }

    [Required]
    public int ActivityId { get; set; }
    public Activity Activity { get; set; } = null!;

    // Registration
    [Required]
    public DateTime RegisteredAt { get; set; }

    //  Attendance
    public DateTime? AttendedAt { get; set; }
}