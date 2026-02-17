using System;
using System.Collections.Generic;
using System.Text;

namespace UniversityActivities.Domain.Entities
{
    public class ReportImage : BaseEntity
    {
        public int ReportId { get; set; }
        public Report Report { get; set; } = null!;

        public string ImageUrl { get; set; } = string.Empty;

        public int DisplayOrder { get; set; }
    }
}
