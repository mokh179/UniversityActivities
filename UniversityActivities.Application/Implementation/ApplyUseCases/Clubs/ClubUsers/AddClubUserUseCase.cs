using System;
using System.Collections.Generic;
using System.Text;
using UniversityActivities.Application.DTOs.Clubs.ClubUsers;
using UniversityActivities.Application.Interfaces.IUnitOfWork;
using UniversityActivities.Application.UseCases.Clubs.Clubusers;
using UniversityActivities.Domain.Enums;

namespace UniversityActivities.Application.Implementation.ApplyUseCases.Clubs.ClubUsers
{
    public class AddClubUserUseCase : IAddClubUserUseCase
    {
        private readonly IUnitOfWork _uow;

        public AddClubUserUseCase(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task ExecuteAsync(
            AddClubUserDto dto,
            CancellationToken ct)
        {
            var club = await _uow.Clubs
                .GetByIdAsync(dto.ClubId, ct);

            if (club == null)
                throw new Exception("Club not found.");

            var exists = await _uow.ClubUsers
                .ExistsAsync(dto.ClubId, dto.UserId, ct);

            if (exists)
                throw new Exception("User already member in club.");

            if (dto.Role == ClubRole.President)
            {
                var hasPresident = await _uow.ClubUsers
                    .HasPresidentAsync(dto.ClubId, ct);

                if (hasPresident)
                    throw new Exception("Club already has a President.");
            }

            var entity = new ClubMember
            {
                ClubId = dto.ClubId,
                UserId = dto.UserId,
                Role = dto.Role
            };

            await _uow.ClubUsers.AddAsync(entity, ct);
            await _uow.SaveChangesAsync(ct);
        }
    }

}
