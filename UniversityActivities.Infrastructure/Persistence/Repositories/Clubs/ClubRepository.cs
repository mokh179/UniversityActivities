using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using UniversityActivities.Application.Common.Models;
using UniversityActivities.Application.Interfaces.Repositories.Clubs;
using UniversityActivities.Domain.Entities;

namespace UniversityActivities.Infrastructure.Persistence.Repositories.Clubs
{
    public class ClubRepository:IClubRepository
    {
        private readonly AppDbContext _context;

        public ClubRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Club club, CancellationToken ct)
        {
            await _context.Clubs.AddAsync(club, ct);
        }

        public async Task<Club?> GetByIdAsync(int id, CancellationToken ct)
        {
            return await _context.Clubs
                .Include(x => x.ClubDomain)
                .Include(x => x.Management)
                .FirstOrDefaultAsync(x => x.Id == id && x.IsActive, ct);
        }

        public async Task<PagedResult<Club>> GetPagedAsync(
            ClubQueryFilter filter,
            CancellationToken ct)
        {
            var query = _context.Clubs
                .Include(x => x.ClubDomain)
                .Include(x => x.Management)
                .Where(x => x.IsActive)
                .AsQueryable();

            if (filter.ManagementId.HasValue)
                query = query.Where(x => x.ManagementId == filter.ManagementId);

            if (filter.DomainId.HasValue)
                query = query.Where(x => x.ClubDomainId == filter.DomainId);

            if (!string.IsNullOrWhiteSpace(filter.Search))
                query = query.Where(x =>
                    x.NameAr.Contains(filter.Search) ||
                    x.NameEn.Contains(filter.Search));

            var total = await query.CountAsync(ct);

            var items = await query
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .ToListAsync(ct);

            return new PagedResult<Club>
            (
                items,
                total,
                filter.PageNumber,
                filter.PageSize
            );
        }

        public Task SoftDeleteAsync(Club club)
        {
            club.IsActive = false;
            _context.Clubs.Update(club);
            return Task.CompletedTask;
        }

        public Task UpdateAsync(Club club)
        {
            _context.Clubs.Update(club);
            return Task.CompletedTask;
        }
    }
}
