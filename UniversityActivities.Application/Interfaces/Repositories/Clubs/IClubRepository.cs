using System;
using System.Collections.Generic;
using System.Text;
using UniversityActivities.Application.Common.Models;
using UniversityActivities.Domain.Entities;

namespace UniversityActivities.Application.Interfaces.Repositories.Clubs
{
    public interface IClubRepository
    {
        Task AddAsync(Club club, CancellationToken ct);

        Task<Club?> GetByIdAsync(int id, CancellationToken ct);

        Task<PagedResult<Club>> GetPagedAsync(
            ClubQueryFilter filter,
            CancellationToken ct);

        Task SoftDeleteAsync(Club club);

        Task UpdateAsync(Club club);
    }
}
