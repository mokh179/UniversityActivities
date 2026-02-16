using System;
using System.Collections.Generic;
using System.Text;
using UniversityActivities.Domain.Entities;

namespace UniversityActivities.Application.Interfaces.Repositories.Clubs.ClubUsers
{
    public interface IClubJoinRequestRepository
    {
        Task<bool> ExistsPendingAsync(
            int clubId,
            int userId,
            CancellationToken ct);

        Task AddAsync(
            ClubJoinRequest entity,
            CancellationToken ct);

        Task<ClubJoinRequest?> GetAsync(
            int requestId,
            CancellationToken ct);

        Task<List<ClubJoinRequest>> GetPendingByClubAsync(
            int clubId,
            CancellationToken ct);
    }
}
