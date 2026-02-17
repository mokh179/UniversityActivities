using System;
using System.Collections.Generic;
using System.Text;

namespace UniversityActivities.Application.DTOs.Reports
{
    public class ReportDetailsDto
    {
        public int Id { get; set; }

        public string TitleAr { get; set; } = string.Empty;
        public string TitleEn { get; set; } = string.Empty;

        public string SummaryAr { get; set; } = string.Empty;
        public string SummaryEn { get; set; } = string.Empty;

        public string ContentAr { get; set; } = string.Empty;
        public string ContentEn { get; set; } = string.Empty;

        public DateTime EventDate { get; set; }

        public bool IsFeatured { get; set; }

        public int ViewCount { get; set; }

        public string? ClubNameAr { get; set; }
        public string? ClubNameEn { get; set; }
        public string? ActivityTitleAr { get; set; }
        public string? ActivityTitleEn { get; set; }

        public List<string> ImageUrls { get; set; } = new();
        public List<string> AttachmentUrls { get; set; } = new();
    }
}
