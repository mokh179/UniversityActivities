using System;
using System.Collections.Generic;
using System.Text;

namespace UniversityActivities.Application.UseCases.Clubs
{
    public interface IDeleteClubUseCase
    {
        Task ExecuteAsync(
            int clubId,
            CancellationToken ct);
    }
}
