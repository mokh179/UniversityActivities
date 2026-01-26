
public class StudentActivity : AuditableEntity
{
    [Required]
    public int StudentId { get; set; }

    [Required]
    public int ActivityId { get; set; }

    // Registration
    [Required]
    public DateTime RegisteredAt { get; set; }

    //  Attendance
    public DateTime? AttendedAt { get; set; }
}