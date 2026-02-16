using System;
using System.Collections.Generic;
using System.Text;

namespace UniversityActivities.Application.UserCases.Activities.Admin
{
    public interface IPublishActivityUseCase
    {
        Task ExecuteAsync(int activityId, bool isPublished);
    }
}
