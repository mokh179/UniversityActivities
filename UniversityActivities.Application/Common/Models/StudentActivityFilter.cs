using System;
using System.Collections.Generic;
using System.Text;

namespace UniversityActivities.Application.Common.Models
{
    public class StudentActivityFilter
    {
        // Search by title (AR / EN)
        public string? Title { get; set; }

        //  Filter by management (college / deanship)
        public int? ManagementId { get; set; }

        // Filter by attendance mode (Onsite / Online)
        public int? AttendanceModeId { get; set; }

        //  Filter by activity date range
        public DateTime? StartDateFrom { get; set; }
        public DateTime? StartDateTo { get; set; }
    }
}
