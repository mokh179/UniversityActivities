using UniversityActivities.Application.DTOs.Activities;

public interface IActivityAssignmentRepository
{
    Task ReplaceAsync(
        int activityId,
        List<ActivityAssignmentDto> assignments);
}
