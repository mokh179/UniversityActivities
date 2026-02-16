using System;
using System.Collections.Generic;
using System.Text;
using UniversityActivities.Application.DTOs.Clubs;

namespace UniversityActivities.Application.UseCases.Clubs
{
    public interface IGetClubByIdUseCase
    {
        Task<ClubDetailsDto?> ExecuteAsync(
            int clubId,
            CancellationToken ct);
    }
}
