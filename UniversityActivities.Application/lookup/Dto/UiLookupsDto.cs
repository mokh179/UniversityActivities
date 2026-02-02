using System;
using System.Collections.Generic;
using System.Text;

namespace UniversityActivities.Application.lookup.Dto
{
    public class UiLookupsDto
    {
        public List<LookupDto> Managements { get; set; } = new();
        public List<LookupDto> ActivityTypes { get; set; } = new();
        public List<LookupDto> AttendanceScopes { get; set; } = new();
        public List<LookupDto> TargetAudiences { get; set; } = new();
    }

    public class LookupDto
    {
        public int Id { get; set; }
        public string NameAr { get; set; } = string.Empty;
        public string NameEn { get; set; } = string.Empty;
    }
}
