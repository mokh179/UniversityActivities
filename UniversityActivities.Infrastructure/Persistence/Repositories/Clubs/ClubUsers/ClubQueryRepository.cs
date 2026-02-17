using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using UniversityActivities.Application.DTOs.Clubs.ClubUsers;
using UniversityActivities.Application.Interfaces.Repositories.Clubs.ClubUsers;
using UniversityActivities.Domain.Enums;

namespace UniversityActivities.Infrastructure.Persistence.Repositories.Clubs.ClubUsers
{
    public class ClubQueryRepository : IClubQueryRepository
    {
        private readonly AppDbContext _context;

        public ClubQueryRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ClubStatsDto> GetClubStatsAsync(
            int clubId,
            CancellationToken ct)
        {
            var totalMembers = await _context.ClubMembers
                .CountAsync(x => x.ClubId == clubId, ct);

            var pending = await _context.ClubJoinRequests
                .CountAsync(x =>
                    x.ClubId == clubId &&
                    x.Status == JoinRequestStatus.Pending, ct);

            var rejected = await _context.ClubJoinRequests
                .CountAsync(x =>
                    x.ClubId == clubId &&
                    x.Status == JoinRequestStatus.Rejected, ct);

            return new ClubStatsDto
            {
                TotalMembers = totalMembers,
                PendingRequests = pending,
                RejectedRequests = rejected
            };
        }
    }

}
