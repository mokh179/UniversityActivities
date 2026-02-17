using System;
using System.Collections.Generic;
using System.Text;
using UniversityActivities.Application.Interfaces.IUnitOfWork;
using UniversityActivities.Application.UseCases.Reports;

namespace UniversityActivities.Application.Implementation.ApplyUseCases.Reports
{
    public class DeleteReportUseCase : IDeleteReportUseCase
    {
        private readonly IUnitOfWork _uow;

        public DeleteReportUseCase(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task ExecuteAsync(
            int reportId,
            CancellationToken ct)
        {
            var report = await _uow.Reports
                .GetByIdAsync(reportId, ct);

            if (report == null)
                throw new Exception("Report not found");

            await _uow.Reports.SoftDeleteAsync(report); 

            await _uow.SaveChangesAsync(ct);
        }
    }

}
