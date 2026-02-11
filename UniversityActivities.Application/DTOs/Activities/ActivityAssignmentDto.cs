namespace UniversityActivities.Application.DTOs.Activities;

public class ActivityAssignmentDto
{
    public int UserId { get; set; }
    public int ActivityRoleId { get; set; }
    public bool? Isnew { get; set; }
}
