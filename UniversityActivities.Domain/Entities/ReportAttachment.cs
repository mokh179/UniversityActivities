using System;
using System.Collections.Generic;
using System.Text;

namespace UniversityActivities.Domain.Entities
{
    public class ReportAttachment : BaseEntity
    {
        public int ReportId { get; set; }
        public Report Report { get; set; } = null!;

        public string FileName { get; set; } = string.Empty;

        public string FileUrl { get; set; } = string.Empty;
    }

}
