using System;
using System.Collections.Generic;
using System.Text;

namespace UniversityActivities.Application.UserCases.Activities.Admin
{
    public interface IAssignManagementSupervisorUseCase
    {
        Task ExecuteAsync(
        int currentUserId,
        AssignManagementSupervisorCommand command);
    }
}
