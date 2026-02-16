using System;
using System.Collections.Generic;
using System.Text;

namespace UniversityActivities.Application.UseCases.Clubs.ClubJoinRequest
{
    public interface ISubmitClubJoinRequestUseCase
    {
        Task ExecuteAsync(
     int clubId,
     int userId,
     CancellationToken ct);
    }
}
