using System;
using System.Collections.Generic;
using System.Text;
using UniversityActivities.Application.Interfaces.IUnitOfWork;
using UniversityActivities.Application.UseCases.Clubs.Clubusers;
using UniversityActivities.Domain.Enums;

namespace UniversityActivities.Application.Implementation.ApplyUseCases.Clubs.ClubUsers
{
    public class RemoveClubUserUseCase : IRemoveClubUserUseCase
    {
        private readonly IUnitOfWork _uow;

        public RemoveClubUserUseCase(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task ExecuteAsync(
            int clubId,
            int userId,
            CancellationToken ct)
        {
            var entity = await _uow.ClubUsers
                .GetAsync(clubId, userId, ct);

            if (entity == null)
                throw new Exception("User not found in club.");

            if (entity.Role == ClubRole.President)
            {
                throw new Exception("Cannot remove President directly.");
            }

            await  _uow.ClubUsers.RemoveAsync(entity);

            await _uow.SaveChangesAsync(ct);
        }
    }

}
