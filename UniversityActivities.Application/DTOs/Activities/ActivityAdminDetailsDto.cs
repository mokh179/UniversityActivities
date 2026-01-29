namespace UniversityActivities.Application.DTOs.Activities;

public class ActivityAdminDetailsDto
{
    public int Id { get; set; }

    public string TitleAr { get; set; } = null!;
    public string TitleEn { get; set; } = null!;

    public string DescriptionAr { get; set; } = null!;
    public string DescriptionEn { get; set; } = null!;

    public bool IsPublished { get; set; }

    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public string? LocationAr { get; set; }
    public string? LocationEn { get; set; }
    public string? OnlineLink { get; set; }

    public string ManagementNameAr { get; set; } = null!;
    public string ManagementNameEn { get; set; } = null!;

    public string? ClubNameAr { get; set; }
    public string? ClubNameEn { get; set; }

    public string ActivityTypeNameAr { get; set; } = null!;
    public string ActivityTypeNameEn { get; set; } = null!;

    public string AttendanceModeNameAr { get; set; } = null!;
    public string AttendanceModeNameEn { get; set; } = null!;

    public List<string> TargetAudiencesAr { get; set; } = new();
    public List<string> TargetAudiencesEn { get; set; } = new();
    public string ImageUrl { get; set; } 

    public List<ActivityUserViewDto> Supervisors { get; set; } = new();
    public List<ActivityUserViewDto> Coordinators { get; set; } = new();
    public List<ActivityUserViewDto> Viewers { get; set; } = new();
}
