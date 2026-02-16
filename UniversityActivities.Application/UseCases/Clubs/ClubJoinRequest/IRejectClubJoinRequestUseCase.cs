using System;
using System.Collections.Generic;
using System.Text;

namespace UniversityActivities.Application.UseCases.Clubs.ClubJoinRequest
{
    public interface IRejectClubJoinRequestUseCase
    {
        Task ExecuteAsync(
            int requestId,
            string reason,
            CancellationToken ct);
    }
}
