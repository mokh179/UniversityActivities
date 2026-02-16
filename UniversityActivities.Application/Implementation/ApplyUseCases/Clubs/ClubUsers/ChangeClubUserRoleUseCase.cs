using System;
using System.Collections.Generic;
using System.Text;
using UniversityActivities.Application.Interfaces.IUnitOfWork;
using UniversityActivities.Application.UseCases.Clubs.Clubusers;
using UniversityActivities.Domain.Enums;

namespace UniversityActivities.Application.Implementation.ApplyUseCases.Clubs.ClubUsers
{
    public class ChangeClubUserRoleUseCase : IChangeClubUserRoleUseCase
    {
        private readonly IUnitOfWork _uow;

        public ChangeClubUserRoleUseCase(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task ExecuteAsync(
            int clubId,
            int userId,
            ClubRole newRole,
            CancellationToken ct)
        {
            var entity = await _uow.ClubUsers
                .GetAsync(clubId, userId, ct);

            if (entity == null)
                throw new Exception("User not found.");

            if (newRole == ClubRole.President)
            {
                var hasPresident = await _uow.ClubUsers
                    .HasPresidentAsync(clubId, ct);

                if (hasPresident)
                    throw new Exception("Club already has a President.");
            }

            entity.Role = newRole;

            await _uow.SaveChangesAsync(ct);
        }
    }

}
