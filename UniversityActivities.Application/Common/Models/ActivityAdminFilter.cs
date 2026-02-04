namespace UniversityActivities.Application.Common.Models;

public class ActivityAdminFilter
{
    public string? Title { get; set; }
    public bool? IsPublished { get; set; }

    public int? ManagementId { get; set; }

    public DateTime? StartDateFrom { get; set; }
    public DateTime? StartDateTo { get; set; }




    public int? CoordinatorId { get; set; }
    public int? ParticipantId { get; set; }

    public int? Status { get; set; }

    public int? CategoryId { get; set; }
    public int? ActivityTypeId { get; set; }
    public int? SectionId { get; set; }

    public int PageSize { get; set; } = 10;
}
