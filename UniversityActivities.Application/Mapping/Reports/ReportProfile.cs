using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using UniversityActivities.Application.DTOs.Reports;
using UniversityActivities.Domain.Entities;

namespace UniversityActivities.Application.Mapping.Reports
{
    public class ReportProfile:Profile
    {
        public ReportProfile()
        {
            MapCreateOrUpdate();
        }

        private void MapCreateOrUpdate() {

            // =========================
            // DTO → Entity
            // =========================
            CreateMap<CreateOrUpdateReportDto, Report>()
                .ForMember(d => d.Id, opt => opt.Ignore())
                .ForMember(d => d.Images, opt => opt.Ignore())
                .ForMember(d => d.Attachments, opt => opt.Ignore())
                .ForMember(d => d.ViewCount, opt => opt.Ignore())
                .ForMember(d => d.IsActive, opt => opt.Ignore())
                .ForMember(d => d.Club, opt => opt.Ignore())
                .ForMember(d => d.Activity, opt => opt.Ignore());

            // =========================
            // Entity → List DTO
            // =========================
            CreateMap<Report, ReportListDto>()
                .ForMember(d => d.ClubNameAr,
                    opt => opt.MapFrom(s => s.Club != null ? s.Club.NameAr : null))
                .ForMember(d => d.ClubNameEn,
                    opt => opt.MapFrom(s => s.Club != null ? s.Club.NameEn : null))
                .ForMember(d => d.ActivityTitleAr,
                    opt => opt.MapFrom(s => s.Activity != null ? s.Activity.TitleAr : null))
                .ForMember(d => d.ActivityTitleEn,
                    opt => opt.MapFrom(s => s.Activity != null ? s.Activity.TitleEn : null))
                .ForMember(d => d.MainImageUrl,
                      opt => opt.MapFrom(s => s.Images
                   .OrderBy(x => x.DisplayOrder)
            .Select(x => x.ImageUrl)
            .FirstOrDefault()));

            // =========================
            // Entity → Details DTO
            // =========================
            CreateMap<Report, ReportDetailsDto>()
                .ForMember(d => d.ClubNameAr,
                    opt => opt.MapFrom(s => s.Club != null ? s.Club.NameAr : null))
                .ForMember(d => d.ActivityTitleAr,
                    opt => opt.MapFrom(s => s.Activity != null ? s.Activity.TitleAr : null))
                .ForMember(d => d.ImageUrls,
                    opt => opt.MapFrom(s => s.Images
                        .OrderBy(x => x.DisplayOrder)
                        .Select(x => x.ImageUrl)))
                .ForMember(d => d.AttachmentUrls,
                    opt => opt.MapFrom(s => s.Attachments
                        .Select(x => x.FileUrl)));

            // =========================
            // Entity → CreateOrUpdate DTO (لـ Edit)
            // =========================
            CreateMap<Report, CreateOrUpdateReportDto>()
                .ForMember(d => d.ImageUrls,
                    opt => opt.MapFrom(s => s.Images
                        .OrderBy(x => x.DisplayOrder)
                        .Select(x => x.ImageUrl)))
                .ForMember(d => d.AttachmentUrls,
                    opt => opt.MapFrom(s => s.Attachments
                        .Select(x => x.FileUrl)));
        }
    }
}
