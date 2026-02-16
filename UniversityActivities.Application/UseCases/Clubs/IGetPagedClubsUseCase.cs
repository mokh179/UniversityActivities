using System;
using System.Collections.Generic;
using System.Text;
using UniversityActivities.Application.Common.Models;
using UniversityActivities.Application.DTOs.Clubs;

namespace UniversityActivities.Application.UseCases.Clubs
{
    public interface IGetPagedClubsUseCase
    {
        Task<PagedResult<ClubListDto>> ExecuteAsync(
            ClubQueryFilter filter,
            CancellationToken ct);
    }
}
