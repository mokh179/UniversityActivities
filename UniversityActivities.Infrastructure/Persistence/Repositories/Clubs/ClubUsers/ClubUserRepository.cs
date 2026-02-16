using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using UniversityActivities.Application.DTOs.Clubs.ClubUsers;
using UniversityActivities.Application.Interfaces.Repositories.Clubs.ClubUsers;
using UniversityActivities.Domain.Enums;

namespace UniversityActivities.Infrastructure.Persistence.Repositories.Clubs.ClubMembers
{
    public class ClubUserRepository : IClubUserRepository
    {
        private readonly AppDbContext _context;

        public ClubUserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> ExistsAsync(
            int clubId,
            int userId,
            CancellationToken ct)
        {
            return await _context.ClubMembers
                .AnyAsync(x =>
                    x.ClubId == clubId &&
                    x.UserId == userId, ct);
        }

        public async Task<bool> HasPresidentAsync(
            int clubId,
            CancellationToken ct)
        {
            return await _context.ClubMembers
                .AnyAsync(x =>
                    x.ClubId == clubId &&
                    x.Role == ClubRole.President, ct);
        }

        public async Task<ClubMember?> GetAsync(
            int clubId,
            int userId,
            CancellationToken ct)
        {
            return await _context.ClubMembers
                .FirstOrDefaultAsync(x =>
                    x.ClubId == clubId &&
                    x.UserId == userId, ct);
        }

      

        public async Task AddAsync(
            ClubMember entity,
            CancellationToken ct)
        {
            await _context.ClubMembers
                .AddAsync(entity, ct);
        }

        public Task RemoveAsync(ClubMember entity)
        {
            _context.ClubMembers.Remove(entity);
            return Task.CompletedTask;
        }

        public async Task<List<ClubUserDto>> GetMembersWithDetailsAsync(
    int clubId,
    CancellationToken ct)
        {
            return await _context.ClubMembers
                .Where(x => x.ClubId == clubId)
                .Join(_context.Users,
                    cu => cu.UserId,
                    u => u.Id,
                    (cu, u) => new ClubUserDto
                    {
                        UserId = u.Id,
                        FullName = u.FirstName +' '+u.MiddleName+' '+u.LastName,
                        Email = u.Email,
                        Role = cu.Role
                    })
                .ToListAsync(ct);
        }
    }

}
