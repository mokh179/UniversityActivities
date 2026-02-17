using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using UniversityActivities.Application.Interfaces.Repositories.Reports;
using UniversityActivities.Domain.Entities;

namespace UniversityActivities.Infrastructure.Persistence.Repositories
{
    public class ReportRepository : IReportRepository
    {
        private readonly AppDbContext _context;

        public ReportRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Report report, CancellationToken ct)
        {
            await _context.Reports.AddAsync(report, ct);
        }

        public async Task<Report?> GetByIdAsync(int id, CancellationToken ct)
        {
            return await _context.Reports
                .Include(x => x.Images)
                .Include(x => x.Attachments)
                .Include(x => x.Club)
                .Include(x => x.Activity)
                .FirstOrDefaultAsync(x => x.Id == id, ct);
        }

        public async Task<List<Report>> GetAllAsync(CancellationToken ct)
        {
            return await _context.Reports
                .Include(x => x.Club)
                .Include(x => x.Activity)
                .ToListAsync(ct);
        }

        public async Task<List<Report>> GetPublishedAsync(CancellationToken ct)
        {
            return await _context.Reports
                .Where(x => x.IsPublished)
                .Include(x => x.Images)
                .Include(x => x.Club)
                .Include(x => x.Activity)
                .ToListAsync(ct);
        }

        public Task SoftDeleteAsync(Report report)
        {
            report.IsDeleted = true;
            return Task.CompletedTask;
        }

        public Task UpdateAsync(Report report)
        {
            _context.Reports.Update(report);
            return Task.CompletedTask;
        }

        public async Task IncrementViewCountAsync(int reportId, CancellationToken ct)
        {
            await _context.Reports
                .Where(x => x.Id == reportId)
                .ExecuteUpdateAsync(s =>
                    s.SetProperty(p => p.ViewCount, p => p.ViewCount + 1),
                    ct);
        }
    }

}
