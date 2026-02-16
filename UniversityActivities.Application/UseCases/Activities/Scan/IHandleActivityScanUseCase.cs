using System;
using System.Collections.Generic;
using System.Text;
using UniversityActivities.Application.DTOs.Scan.Models;

namespace UniversityActivities.Application.UseCases.Activities.Scan
{
    public interface IHandleActivityScanUseCase
    {
        Task<ActivityScanResult> ExecuteAsync(
            int activityId,
            int? userId,
            CancellationToken cancellationToken);
    }
}
