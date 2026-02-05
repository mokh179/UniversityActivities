using System;
using System.Collections.Generic;
using System.Text;

namespace UniversityActivities.Application.DTOs.Activities
{
    public class ActivityParticipants
    {
        public List<ParticipantsDto> Coordinators { get; set; }
        public List<ParticipantsDto> Viewer { get; set; }
        public List<ParticipantsDto> Supervisors { get; set; }
    }
}
