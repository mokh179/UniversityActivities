using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using UniversityActivities.Application.DTOs.Clubs;
using UniversityActivities.Application.Interfaces.IUnitOfWork;
using UniversityActivities.Application.UseCases.Clubs;

namespace UniversityActivities.Application.Implementation.ApplyUseCases.Clubs
{
    public class UpdateClubUseCase : IUpdateClubUseCase
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public UpdateClubUseCase(
            IUnitOfWork uow,
            IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task ExecuteAsync(
            int clubId,
            CreateOrUpdateClubDto dto,
            CancellationToken ct)
        {
            var club = await _uow.Clubs
                .GetByIdAsync(clubId, ct);

            if (club == null)
                throw new Exception("Club not found");

            _mapper.Map(dto, club);

            await _uow.SaveChangesAsync(ct);
        }
    }
}
