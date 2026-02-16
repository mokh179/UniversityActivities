using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using UniversityActivities.Application.Common.Models;
using UniversityActivities.Application.DTOs.Clubs;
using UniversityActivities.Application.Interfaces.IUnitOfWork;
using UniversityActivities.Application.UseCases.Clubs;

namespace UniversityActivities.Application.Implementation.ApplyUseCases.Clubs
{
    public class GetPagedClubsUseCase : IGetPagedClubsUseCase
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public GetPagedClubsUseCase(
            IUnitOfWork uow,
            IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<PagedResult<ClubListDto>> ExecuteAsync(
            ClubQueryFilter filter,
            CancellationToken ct)
        {
            var result = await _uow.Clubs
                .GetPagedAsync(filter, ct);

            return result;
        }
    }

}
