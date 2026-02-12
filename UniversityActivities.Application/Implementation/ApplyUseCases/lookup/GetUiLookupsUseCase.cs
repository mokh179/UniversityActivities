using System;
using System.Collections.Generic;
using System.Text;
using UniversityActivities.Application.lookup.Dto;
using UniversityActivities.Application.lookup.Interface;
using UniversityActivities.Application.UserCases.Lookup;

namespace UniversityActivities.Application.Implementation.ApplyUseCases.lookup
{
    public class GetUiLookupsUseCase:IGetUiLookupsUseCase
    {
        private readonly IUiLookupsQuery _query;

        public GetUiLookupsUseCase(IUiLookupsQuery query)
        {
            _query = query;
        }

        public Task<UiLookupsDto> ExecuteAsync()
            => _query.GetAllAsync();
    }
}
