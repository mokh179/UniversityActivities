using System;
using System.Collections.Generic;
using System.Text;
using UniversityActivities.Application.DTOs.Clubs.ClubUsers;

namespace UniversityActivities.Application.Interfaces.Repositories.Clubs.ClubUsers
{
    public interface IClubQueryRepository
    {
        Task<ClubStatsDto> GetClubStatsAsync(
        int clubId,
        CancellationToken ct);
    }
}
