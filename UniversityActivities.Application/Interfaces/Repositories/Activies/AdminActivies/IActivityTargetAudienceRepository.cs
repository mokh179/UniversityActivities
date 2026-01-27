public interface IActivityTargetAudienceRepository
{
    Task ReplaceAsync(int activityId, List<int> targetAudienceIds);
}
