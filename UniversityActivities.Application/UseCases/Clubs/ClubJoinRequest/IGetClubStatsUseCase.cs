using System;
using System.Collections.Generic;
using System.Text;
using UniversityActivities.Application.DTOs.Clubs.ClubUsers;

namespace UniversityActivities.Application.UseCases.Clubs.ClubJoinRequest
{
    public interface IGetClubStatsUseCase
    {
        Task<ClubStatsDto> ExecuteAsync(
            int clubId,
            CancellationToken ct);
    }
}
