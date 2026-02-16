using System;
using System.Collections.Generic;
using System.Text;
using UniversityActivities.Application.Interfaces.IUnitOfWork;
using UniversityActivities.Application.UseCases.Clubs;

namespace UniversityActivities.Application.Implementation.ApplyUseCases.Clubs
{
    public class DeleteClubUseCase : IDeleteClubUseCase
    {
        private readonly IUnitOfWork _uow;

        public DeleteClubUseCase(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task ExecuteAsync(
            int clubId,
            CancellationToken ct)
        {
            var club = await _uow.Clubs
                .GetByIdAsync(clubId, ct);

            if (club == null)
                throw new Exception("Club not found");

            await _uow.Clubs.SoftDeleteAsync(club);
            await _uow.SaveChangesAsync(ct);
        }
    }

}
