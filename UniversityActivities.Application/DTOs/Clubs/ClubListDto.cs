using System;
using System.Collections.Generic;
using System.Text;

namespace UniversityActivities.Application.DTOs.Clubs
{
    public class ClubListDto
    {
        public int Id { get; set; }

        public string NameAr { get; set; } = string.Empty;
        public string NameEn { get; set; } = string.Empty;
        public string DescAr { get; set; } = string.Empty;
        public string DescEn { get; set; } = string.Empty;

        public string DomainNameAr { get; set; } = string.Empty;
        public string DomainNameEn { get; set; } = string.Empty;
        public string ManagementNameAr { get; set; } = string.Empty;
        public string ManagementNameEn { get; set; } = string.Empty;

        public bool IsActive { get; set; }
    }
}
