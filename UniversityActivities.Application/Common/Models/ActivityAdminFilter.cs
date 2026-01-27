namespace UniversityActivities.Application.Common.Models;

public class ActivityAdminFilter
{
    public bool? IsPublished { get; set; }

    public int? ManagementId { get; set; }

    public DateTime? StartDateFrom { get; set; }
    public DateTime? StartDateTo { get; set; }
}
