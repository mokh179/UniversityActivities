using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using UniversityActivities.Application.Interfaces.Repositories.Clubs.ClubUsers;
using UniversityActivities.Domain.Entities;
using UniversityActivities.Domain.Enums;

namespace UniversityActivities.Infrastructure.Persistence.Repositories.Clubs.ClubUsers
{
    internal class ClubJoinRequestRepository: IClubJoinRequestRepository
    {
        private readonly AppDbContext _context;

        public ClubJoinRequestRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> ExistsPendingAsync(
            int clubId,
            int userId,
            CancellationToken ct)
        {
            return await _context.ClubJoinRequests
                .AnyAsync(x =>
                    x.ClubId == clubId &&
                    x.UserId == userId &&
                    x.Status == JoinRequestStatus.Pending,
                    ct);
        }

        public async Task AddAsync(
            ClubJoinRequest entity,
            CancellationToken ct)
        {
            await _context.ClubJoinRequests
                .AddAsync(entity, ct);
        }

        public async Task<ClubJoinRequest?> GetAsync(
            int requestId,
            CancellationToken ct)
        {
            return await _context.ClubJoinRequests
                .FirstOrDefaultAsync(x =>
                    x.Id == requestId,
                    ct);
        }

        public async Task<ClubJoinRequest?> GetByClubAndUserAsync(
            int clubId,
            int userId,
            CancellationToken ct)
        {
            return await _context.ClubJoinRequests
                .FirstOrDefaultAsync(x =>
                    x.ClubId == clubId &&
                    x.UserId == userId,
                    ct);
        }

        public async Task<List<ClubJoinRequest>> GetPendingByClubAsync(
            int clubId,
            CancellationToken ct)
        {
            return await _context.ClubJoinRequests
                .Where(x =>
                    x.ClubId == clubId &&
                    x.Status == JoinRequestStatus.Pending)
                .ToListAsync(ct);
        }

        public Task RemoveAsync(
            ClubJoinRequest entity)
        {
            _context.ClubJoinRequests.Remove(entity);
            return Task.CompletedTask;
        }
    }
}
