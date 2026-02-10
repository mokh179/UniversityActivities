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
        public List<LookupDto> AttendanceModes { get; set; } = new();
        public List<LookupDto> TargetAudiences { get; set; } = new();
        public List<LookupDto> ManagementTypes { get; set; } = new();

    }



}
