using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using UniversityActivities.Application.DTOs.Clubs;
using UniversityActivities.Application.Interfaces.IUnitOfWork;
using UniversityActivities.Application.UseCases.Clubs;

namespace UniversityActivities.Application.Implementation.ApplyUseCases.Clubs
{
    public class GetClubByIdUseCase : IGetClubByIdUseCase
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public GetClubByIdUseCase(
            IUnitOfWork uow,
            IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<ClubDetailsDto?> ExecuteAsync(
            int clubId,
            CancellationToken ct)
        {
            var club = await _uow.Clubs
                .GetByIdAsync(clubId, ct);

            if (club == null)
                return null;

            return _mapper.Map<ClubDetailsDto>(club);
        }
    }

}
