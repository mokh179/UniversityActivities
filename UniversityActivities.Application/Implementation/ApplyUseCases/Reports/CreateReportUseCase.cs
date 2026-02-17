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
    public class CreateReportUseCase : ICreateReportUseCase
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public CreateReportUseCase(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<int> ExecuteAsync(
            CreateOrUpdateReportDto dto,
            CancellationToken ct)
        {
            var report = _mapper.Map<Report>(dto);

            if (dto.ImageUrls != null)
            {
                report.Images = dto.ImageUrls
                    .Select((url, index) => new ReportImage
                    {
                        ImageUrl = url,
                        DisplayOrder = index
                    }).ToList();
            }

            if (dto.AttachmentUrls != null)
            {
                report.Attachments = dto.AttachmentUrls
                    .Select(url => new ReportAttachment
                    {
                        FileUrl = url,
                        FileName = Path.GetFileName(url)
                    }).ToList();
            }

            await _uow.Reports.AddAsync(report, ct);
            await _uow.SaveChangesAsync(ct);

            return report.Id;
        }
    }

}
