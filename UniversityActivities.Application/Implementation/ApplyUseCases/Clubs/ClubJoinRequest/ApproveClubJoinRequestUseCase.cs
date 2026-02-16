using System;
using System.Collections.Generic;
using System.Text;
using UniversityActivities.Application.Interfaces.IUnitOfWork;
using UniversityActivities.Application.UseCases.Clubs.ClubJoinRequest;
using UniversityActivities.Domain.Enums;
using UniversityActivities.Domain.Entities;

namespace UniversityActivities.Application.Implementation.ApplyUseCases.Clubs.ClubJoinRequest
{
    public class ApproveClubJoinRequestUseCase
    : IApproveClubJoinRequestUseCase
    {
        private readonly IUnitOfWork _uow;

        public ApproveClubJoinRequestUseCase(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task ExecuteAsync(
            int requestId,
            CancellationToken ct)
        {
            var request = await _uow.ClubJoinRequests
                .GetAsync(requestId, ct);

            if (request == null)
                throw new Exception("Request not found.");

            if (request.Status != JoinRequestStatus.Pending)
                throw new Exception("Already processed.");

            request.Status = JoinRequestStatus.Approved;

            var clubUser = new ClubMember
            {
                ClubId = request.ClubId,
                UserId = request.UserId,
                Role = ClubRole.Member
            };

            await _uow.ClubUsers.AddAsync(clubUser, ct);

            await _uow.SaveChangesAsync(ct);
        }
    }

}
