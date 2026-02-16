using System;
using System.Collections.Generic;
using System.Text;
using UniversityActivities.Application.Interfaces.IUnitOfWork;
using UniversityActivities.Application.UseCases.Clubs.ClubJoinRequest;
using UniversityActivities.Domain.Enums;

namespace UniversityActivities.Application.Implementation.ApplyUseCases.Clubs.ClubJoinRequest
{
    public class RejectClubJoinRequestUseCase
    : IRejectClubJoinRequestUseCase
    {
        private readonly IUnitOfWork _uow;

        public RejectClubJoinRequestUseCase(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task ExecuteAsync(
            int requestId,
            string reason,
            CancellationToken ct)
        {
            var request = await _uow.ClubJoinRequests
                .GetAsync(requestId, ct);

            if (request == null)
                throw new Exception("Request not found.");

            request.Status = JoinRequestStatus.Rejected;
            request.RejectionReason = reason;

            await _uow.SaveChangesAsync(ct);
        }
    }

}
