using System;
using System.Collections.Generic;
using System.Text;
using UniversityActivities.Application.DTOs.Clubs.ClubUsers;

namespace UniversityActivities.Application.Interfaces.Repositories.Clubs.ClubUsers
{
    public interface IClubUserRepository
    {
        Task<bool> ExistsAsync(
            int clubId,
            int userId,
            CancellationToken ct);

        Task<bool> HasPresidentAsync(
            int clubId,
            CancellationToken ct);

        Task<ClubMember?> GetAsync(
            int clubId,
            int userId,
            CancellationToken ct);

        //Task<List<ClubMember>> GetByClubIdAsync(
        //    int clubId,
        //    CancellationToken ct);

        Task AddAsync(
            ClubMember entity,
            CancellationToken ct);

        Task RemoveAsync(
            ClubMember entity);

        Task<List<ClubUserDto>> GetMembersWithDetailsAsync(int clubId, CancellationToken ct);
    }

}
