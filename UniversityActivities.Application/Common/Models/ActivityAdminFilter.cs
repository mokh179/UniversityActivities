namespace UniversityActivities.Application.Common.Models;

public class ActivityAdminFilter
{
    public string Title { get; set; }
    public bool? IsPublished { get; set; }

    public int? ManagementId { get; set; }

    public DateTime? StartDateFrom { get; set; }
    public DateTime? StartDateTo { get; set; }
}
