using System;
using System.Collections.Generic;
using System.Text;
using UniversityActivities.Application.DTOs.Clubs.ClubUsers;
using UniversityActivities.Application.Interfaces.IUnitOfWork;
using UniversityActivities.Application.UseCases.Clubs.Clubusers;

namespace UniversityActivities.Application.Implementation.ApplyUseCases.Clubs.ClubUsers
{
    public class GetClubUsersUseCase : IGetClubUsersUseCase
    {
        private readonly IUnitOfWork _uow;

        public GetClubUsersUseCase(
            IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<List<ClubUserDto>> ExecuteAsync(
            int clubId,
            CancellationToken ct)
        {
            return await _uow.ClubUsers.GetMembersWithDetailsAsync(clubId, ct);
               
        }
    }

}
