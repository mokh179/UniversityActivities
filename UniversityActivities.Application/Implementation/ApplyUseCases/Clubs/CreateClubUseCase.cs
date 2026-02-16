using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using UniversityActivities.Application.DTOs.Clubs;
using UniversityActivities.Application.Interfaces.IUnitOfWork;
using UniversityActivities.Application.UseCases.Clubs;
using UniversityActivities.Domain.Entities;

namespace UniversityActivities.Application.Implementation.ApplyUseCases.Clubs
{
    public class CreateClubUseCase : ICreateClubUseCase
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public CreateClubUseCase(
            IUnitOfWork uow,
            IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<int> ExecuteAsync(
            CreateOrUpdateClubDto dto,
            CancellationToken ct)
        {
            var club = _mapper.Map<Club>(dto);

            await _uow.Clubs.AddAsync(club, ct);
            await _uow.SaveChangesAsync(ct);

            return club.Id;
        }
    }
}
