using System;
using System.Collections.Generic;
using System.Text;

namespace UniversityActivities.Application.DTOs.Reports
{
    public class CreateOrUpdateReportDto
    {
        public int? Id { get; set; }

        public string TitleAr { get; set; } = string.Empty;
        public string TitleEn { get; set; } = string.Empty;

        public string SummaryAr { get; set; } = string.Empty;
        public string SummaryEn { get; set; } = string.Empty;

        public string ContentAr { get; set; } = string.Empty;
        public string ContentEn { get; set; } = string.Empty;

        public DateTime EventDate { get; set; }

        public bool IsFeatured { get; set; }

        public bool IsPublished { get; set; }

        public int? ClubId { get; set; }
        public int? ActivityId { get; set; }

        public List<string>? ImageUrls { get; set; }

        public List<string>? AttachmentUrls { get; set; }
    }
}
