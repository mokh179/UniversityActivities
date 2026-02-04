namespace UniversityActivities.Application.DTOs.Activities;

public class CreateOrUpdateActivityDto
{
    public string TitleAr { get; set; } = null!;
    public string TitleEn { get; set; } = null!;

    public string DescriptionAr { get; set; } = null!;
    public string DescriptionEn { get; set; } = null!;

    public int ManagementId { get; set; }
    public int? ClubId { get; set; }

    public int ActivityTypeId { get; set; }
    public int AttendanceModeId { get; set; }
    public int AttendanceScopeId { get; set; }

    // 🔴 Multiple Target Audiences
    public List<int> TargetAudienceIds { get; set; } = new();

    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public string? LocationAr { get; set; }
    public string? ImageUrl { get; set; }
    public string? LocationEn { get; set; }
    public string? OnlineLink { get; set; }

    public bool IsPublished { get; set; }

    public List<ActivityAssignmentDto> Assignments { get; set; } = new();
}
