using System;
using System.Collections.Generic;
using System.Text;
using UniversityActivities.Application.DTOs.Reports;

namespace UniversityActivities.Application.UseCases.Reports
{
    public interface ICreateReportUseCase
    {
        Task<int> ExecuteAsync(
        CreateOrUpdateReportDto dto,
        CancellationToken ct);
    }
}
