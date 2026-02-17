using System;
using System.Collections.Generic;
using System.Text;
using UniversityActivities.Application.DTOs.Clubs.ClubUsers;
using UniversityActivities.Application.Interfaces.Repositories.Clubs.ClubUsers;
using UniversityActivities.Application.UseCases.Clubs.ClubJoinRequest;

namespace UniversityActivities.Application.Implementation.ApplyUseCases.Clubs.ClubJoinRequest
{
    public class GetClubStatsUseCase : IGetClubStatsUseCase
    {
        private readonly IClubQueryRepository _queryRepo;

        public GetClubStatsUseCase(IClubQueryRepository queryRepo)
        {
            _queryRepo = queryRepo;
        }

        public async Task<ClubStatsDto> ExecuteAsync(
            int clubId,
            CancellationToken ct)
        {
            return await _queryRepo
                .GetClubStatsAsync(clubId, ct);
        }
    }

}
