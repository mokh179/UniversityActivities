using System;
using System.Collections.Generic;
using System.Text;
using UniversityActivities.Domain.Enums;

namespace UniversityActivities.Application.DTOs.Clubs.ClubUsers
{
    public class AddClubUserDto
    {
        public int ClubId { get; set; }
        public int UserId { get; set; }
        public ClubRole Role { get; set; }
    }
}
