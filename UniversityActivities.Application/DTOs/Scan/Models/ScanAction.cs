using System;
using System.Collections.Generic;
using System.Text;

namespace UniversityActivities.Application.DTOs.Scan.Models
{
    public enum ScanAction
    {
        RedirectToLogin,
        ActivityNotStarted,
        RegisterAndAttend,
        MarkAttendance,
        AlreadyAttended,
        RedirectToEvaluation,
        RedirectToCertificate,
        ActivityClosed
    }
}
