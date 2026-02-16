using System;
using System.Collections.Generic;
using System.Text;

namespace UniversityActivities.Application.UseCases.Clubs.Clubusers
{
    public interface IRemoveClubUserUseCase
    {
        Task ExecuteAsync(
            int clubId,
            int userId,
            CancellationToken ct);
    }
}
