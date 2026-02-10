using System;
using System.Collections.Generic;
using System.Text;

namespace UniversityActivities.Application.DTOs.Activities.Student
{
    public class StudentActivityDetailsDto
    {
        public int Id { get; set; }

        public string TitleAr { get; set; } = null!;
        public string TitleEn { get; set; } = null!;

        public string DescriptionEn { get; set; } = null!;
        public string DescriptionAr { get; set; } = null!;
        public string ActivityTypeAr { get; set; } = null!;
        public string ActivityTypeEn { get; set; } = null!;
        public string ScopeEn { get; set; } = null!;
        public string ScopeAr { get; set; } = null!;
        public List<string> TargetAudiencesEn { get; set; } = null!;
        public List<string> TargetAudiencesAr { get; set; } = null!;


        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public string? Location { get; set; }
        public string? OnlineLink { get; set; }

        public string? ImageUrl { get; set; }

        public string ManagementNameAr { get; set; } = null!;
        public string ManagementNameEn { get; set; } = null!;
        public string LocationAr { get; set; } = null!;

        public bool IsRegistered { get; set; }
        public bool IsAttended { get; set; }
        public bool IsRated { get; set; }
        public bool ActivityFinished { get; set; }
    }
}
