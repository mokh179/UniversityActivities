using System;
using System.Collections.Generic;
using System.Text;

namespace UniversityActivities.Application.UseCases.Reports
{
    public interface IDeleteReportUseCase
    {
        Task ExecuteAsync(
            int reportId,
            CancellationToken ct);
    }
}
