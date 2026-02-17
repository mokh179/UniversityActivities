using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using UniversityActivities.Application.DTOs.Reports;
using UniversityActivities.Application.Interfaces.IUnitOfWork;
using UniversityActivities.Application.UseCases.Reports;
using UniversityActivities.Domain.Entities;

namespace UniversityActivities.Application.Implementation.ApplyUseCases.Reports
{
    public class UpdateReportUseCase : IUpdateReportUseCase
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public UpdateReportUseCase(
            IUnitOfWork uow,
            IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task ExecuteAsync(
            int reportId,
            CreateOrUpdateReportDto dto,
            CancellationToken ct)
        {
            var report = await _uow.Reports
                .GetByIdAsync(reportId, ct);

            if (report == null)
                throw new Exception("Report not found");

            // Map basic fields
            _mapper.Map(dto, report);

            // Replace Images
            report.Images.Clear();

            if (dto.ImageUrls != null && dto.ImageUrls.Any())
            {
                report.Images = dto.ImageUrls
                    .Select((url, index) => new ReportImage
                    {
                        ImageUrl = url,
                        DisplayOrder = index
                    })
                    .ToList();
            }

            // Replace Attachments
            report.Attachments.Clear();

            if (dto.AttachmentUrls != null && dto.AttachmentUrls.Any())
            {
                report.Attachments = dto.AttachmentUrls
                    .Select(url => new ReportAttachment
                    {
                        FileUrl = url,
                        FileName = Path.GetFileName(url)
                    })
                    .ToList();
            }

            await _uow.SaveChangesAsync(ct);
        }
    }

}
