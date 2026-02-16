using System;
using System.Collections.Generic;
using System.Text;
using UniversityActivities.Domain.Enums;

namespace UniversityActivities.Application.UseCases.Clubs.Clubusers
{
    public interface IChangeClubUserRoleUseCase
    {
        Task ExecuteAsync(
            int clubId,
            int userId,
            ClubRole newRole,
            CancellationToken ct);
    }
}
