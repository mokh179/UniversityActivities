using System;
using System.Collections.Generic;
using System.Text;
using UniversityActivities.Application.DTOs.Clubs.ClubUsers;

namespace UniversityActivities.Application.UseCases.Clubs.Clubusers
{
    public interface IAddClubUserUseCase
    {
        Task ExecuteAsync(
            AddClubUserDto dto,
            CancellationToken ct);
    }
}
