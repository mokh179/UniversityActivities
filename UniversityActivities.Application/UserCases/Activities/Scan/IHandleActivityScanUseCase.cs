using System;
using System.Collections.Generic;
using System.Text;
using UniversityActivities.Application.DTOs.Scan.Models;

namespace UniversityActivities.Application.UserCases.Activities.Scan
{
    public interface IHandleActivityScanUseCase
    {
        Task<ActivityScanResult> ExecuteAsync(
            int activityId,
            int? userId,
            CancellationToken cancellationToken);
    }
}
