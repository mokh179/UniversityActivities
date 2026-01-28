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

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public string? ImageUrl { get; set; }

        public string ManagementNameAr { get; set; } = null!;
        public string ManagementNameEn { get; set; } = null!;

        public bool IsRegistered { get; set; }
    }
}
