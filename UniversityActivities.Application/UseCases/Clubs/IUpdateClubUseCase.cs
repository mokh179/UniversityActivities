using System;
using System.Collections.Generic;
using System.Text;
using UniversityActivities.Application.DTOs.Clubs;

namespace UniversityActivities.Application.UseCases.Clubs
{
    public interface IUpdateClubUseCase
    {
        Task ExecuteAsync(
            int clubId,
            CreateOrUpdateClubDto dto,
            CancellationToken ct);
    }
}
