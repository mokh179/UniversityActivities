using System;
using System.Collections.Generic;
using System.Text;

namespace UniversityActivities.Application.DTOs.Activities.Student
{
    public class StudentActivityListItemDto
    {
        public int Id { get; set; }

        public string TitleAr { get; set; } = null!;
        public string TitleEn { get; set; } = null!;
        public string StatusEn { get; set; } = null!;
        public string StatusAr { get; set; } = null!;
        public string ActiviyTypeEn { get; set; } = null!;
        public string ActiviyTypeAr { get; set; } = null!;
        public string? LocationAr { get; set; } = null!;
        public string? LocationEn { get; set; } = null!;
        public string? onlineLink { get; set; } = null!;
        public string AttendenceMode { get; set; } = null!;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public string? ImageUrl { get; set; }

        public string ManagementNameAr { get; set; } = null!;
        public string ManagementNameEn { get; set; } = null!;

        public bool IsRegistered { get; set; }
    }
}
