using System;
using System.Collections.Generic;
using System.Text;
using UniversityActivities.Domain.Enums;

namespace UniversityActivities.Application.DTOs.Clubs.ClubUsers
{
    public class ClubUserDto
    {
        public int UserId { get; set; }

        public string FullName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public ClubRole Role { get; set; }
    }
}
