using System;
using System.Collections.Generic;
using System.Text;

namespace UniversityActivities.Domain.Entities
{
    public class Report : AuditableEntity
    {
        public string TitleAr { get; set; } = string.Empty;
        public string TitleEn { get; set; } = string.Empty;

        public string SummaryAr { get; set; } = string.Empty;
        public string SummaryEn { get; set; } = string.Empty;

        public string ContentAr { get; set; } = string.Empty;
        public string ContentEn { get; set; } = string.Empty;

        public DateTime EventDate { get; set; }

        public int ViewCount { get; set; } = 0;

        public bool IsFeatured { get; set; } = false;

        public bool IsPublished { get; set; } = false;

        public bool IsActive { get; set; } = true;

        public int? ClubId { get; set; }
        public Club? Club { get; set; }

        public int? ActivityId { get; set; }
        public Activity? Activity { get; set; }

        public ICollection<ReportImage> Images { get; set; }
            = new List<ReportImage>();

        public ICollection<ReportAttachment> Attachments { get; set; }
            = new List<ReportAttachment>();
    }

}
