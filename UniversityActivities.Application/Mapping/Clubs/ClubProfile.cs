using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using UniversityActivities.Application.DTOs.Clubs;
using UniversityActivities.Domain.Entities;

namespace UniversityActivities.Application.Mapping.NewFolder
{
    public class ClubProfile:Profile
    {
        public ClubProfile()
        {
            MapCreateOrUpdate();
        }

        private void MapCreateOrUpdate()
        {
            // DTO → Entity (Create / Update)
            CreateMap<CreateOrUpdateClubDto, Club>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Activities, opt => opt.Ignore())
                .ForMember(dest => dest.ClubMembers, opt => opt.Ignore())
                .ForMember(dest => dest.Management, opt => opt.Ignore())
                .ForMember(dest => dest.ClubDomain, opt => opt.Ignore());
            //.ForMember(dest => dest.AttendanceScope, opt => opt.Ignore());

            // Entity → Details DTO
            CreateMap<Club, ClubDetailsDTO>()
                .ForMember(dest => dest.ClubDomainNameAr,
                    opt => opt.MapFrom(src => src.ClubDomain.NameAr))
                .ForMember(dest => dest.ClubDomainNameEn,
                    opt => opt.MapFrom(src => src.ClubDomain.NameEn))
                .ForMember(dest => dest.ManagementNameAr,
                    opt => opt.MapFrom(src => src.Management.NameAr))
                .ForMember(dest => dest.ManagementNameEn,
                    opt => opt.MapFrom(src => src.Management.NameEn));
                //.ForMember(dest => dest.AttendanceScopeNameAr,
                //    opt => opt.MapFrom(src => src.AttendanceScope.NameAr));

            // Entity → CreateOrUpdateDto (في حالة Edit Load)
            CreateMap<Club, CreateOrUpdateClubDto>();
        }
    }
}
