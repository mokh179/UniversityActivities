using System;
using System.Collections.Generic;
using System.Text;
using UniversityActivities.Application.DTOs.Enums;
using UniversityActivities.Domain.Entities;

namespace UniversityActivities.Application.Interfaces.Repositories.Activies.Background
{
    public interface IActivityStatusRepository
    {
        Task<List<Activity>> GetUpcomingToStartAsync(
           DateTime now,
           CancellationToken cancellationToken);

        Task<List<Activity>> GetInProgressToCompleteAsync(
            DateTime now,
            CancellationToken cancellationToken);

        Task UpdateStatusAsync(
            IEnumerable<Activity> activities,
            StatusEnums newStatus,
            CancellationToken cancellationToken);
    }
}
