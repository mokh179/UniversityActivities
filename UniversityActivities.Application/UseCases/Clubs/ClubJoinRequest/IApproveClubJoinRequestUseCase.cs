using System;
using System.Collections.Generic;
using System.Text;

namespace UniversityActivities.Application.UseCases.Clubs.ClubJoinRequest
{
    public interface IApproveClubJoinRequestUseCase
    {
        Task ExecuteAsync(
       int requestId,
       CancellationToken ct);
    }
}
