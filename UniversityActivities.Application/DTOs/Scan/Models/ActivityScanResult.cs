using System;
using System.Collections.Generic;
using System.Text;

namespace UniversityActivities.Application.DTOs.Scan.Models
{
    public class ActivityScanResult
    {
            public ScanAction Action { get; set; }

            public string? Message { get; set; }

            public int ActivityId { get; set; }
        
    }
}
