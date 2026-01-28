using System;
using System.Collections.Generic;
using System.Text;

namespace UniversityActivities.Application.DTOs.Evaluation
{
    public class ActivityEvaluationSummaryDto
    {
        public int ActivityId { get; set; }
        public double OverallAverage { get; set; }
        public int TotalEvaluations { get; set; }
    }

}
