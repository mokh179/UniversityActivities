namespace UniversityActivities.Application.DTOs.Activities;

public class ActivityAdminListItemDto
{
    public int Id { get; set; }

    public string TitleAr { get; set; } = null!;
    public string TitleEn { get; set; } = null!;
    public string ActivityTypeNameEn { get; set; } = null!;
    public string ActivityTypeNameAr { get; set; } = null!;
    public string AttendanceModeNameEn { get; set; } = null!;
    public string AttendanceModeNameAr { get; set; } = null!;
    public string ImageUrl { get; set; } = null!;

    public bool IsPublished { get; set; }

    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public string ManagementNameAr { get; set; } = null!;
    public string ManagementNameEn { get; set; } = null!;

    public int RegisteredCount { get; set; }
    public int AttendedCount { get; set; }

    public int SupervisorsCount { get; set; }
    public int CoordinatorsCount { get; set; }
    public int ApproversCount { get; set; }
    public int ViewersCount { get; set; }
}
