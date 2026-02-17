using System;
using System.Collections.Generic;
using System.Text;
using UniversityActivities.Domain.Entities;

namespace UniversityActivities.Application.Interfaces.Repositories.Reports
{
    public interface IReportRepository
    {
        Task AddAsync(Report report, CancellationToken ct);

        Task<Report?> GetByIdAsync(int id, CancellationToken ct);

        Task<List<Report>> GetAllAsync(CancellationToken ct);

        Task<List<Report>> GetPublishedAsync(CancellationToken ct);

        Task SoftDeleteAsync(Report report);

        Task UpdateAsync(Report report);

        Task IncrementViewCountAsync(int reportId, CancellationToken ct);
    }
}
