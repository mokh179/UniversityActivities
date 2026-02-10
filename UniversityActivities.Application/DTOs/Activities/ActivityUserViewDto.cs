namespace UniversityActivities.Application.DTOs.Activities;

public class ActivityUserViewDto
{
    public int UserId { get; set; }

    public string FullNameAr { get; set; } = null!;
    public string FullNameEn { get; set; } = null!;
}
