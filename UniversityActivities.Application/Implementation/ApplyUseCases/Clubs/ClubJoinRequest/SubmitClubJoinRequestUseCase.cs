using System;
using System.Collections.Generic;
using System.Text;
using UniversityActivities.Application.Interfaces.IUnitOfWork;
using UniversityActivities.Application.UseCases.Clubs.ClubJoinRequest;

namespace UniversityActivities.Application.Implementation.ApplyUseCases.Clubs.ClubJoinRequest
{
    public class SubmitClubJoinRequestUseCase
    : ISubmitClubJoinRequestUseCase
    {
        private readonly IUnitOfWork _uow;

        public SubmitClubJoinRequestUseCase(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task ExecuteAsync(
            int clubId,
            int userId,
            CancellationToken ct)
        {
            var club = await _uow.Clubs.GetByIdAsync(clubId, ct);
            if (club == null)
                throw new Exception("Club not found.");

            var alreadyMember = await _uow.ClubUsers
                .ExistsAsync(clubId, userId, ct);

            if (alreadyMember)
                throw new Exception("Already a member.");

            var pending = await _uow.ClubJoinRequests
                .ExistsPendingAsync(clubId, userId, ct);

            if (pending)
                throw new Exception("Request already submitted.");

            var request = new Domain.Entities.ClubJoinRequest
            {
                ClubId = clubId,
                UserId = userId
            };

            await _uow.ClubJoinRequests.AddAsync(request, ct);
            await _uow.SaveChangesAsync(ct);
        }
    }

}
