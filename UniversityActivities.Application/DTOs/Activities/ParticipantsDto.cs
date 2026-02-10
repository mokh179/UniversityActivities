using System;
using System.Collections.Generic;
using System.Text;

namespace UniversityActivities.Application.DTOs.Activities
{
    public class ParticipantsDto
    {
        public int Id { get; set; }
        public string Fullname { get; set; }=string.Empty;  
        public string Useraname { get; set; }=string.Empty;  
        public string RoleName { get; set; }=string.Empty;  
    }
}
