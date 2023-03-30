using AutoMapper;
using HexaShop.Application.Dtos.DetailDtos.Commands;
using HexaShop.Application.Dtos.DetailDtos.Queries;
using HexaShop.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexaShop.Application.MappingProfiles
{
    public class DetailProfile : Profile
    {
        public DetailProfile()
        {
            CreateMap<CreateDetailDto, Detail>()
                .ForMember(dest => dest.Key, _ => _.MapFrom(src => src.Key))
                .ForMember(dest => dest.Value, _ => _.MapFrom(src => src.Value));


            CreateMap<Detail, DetailDto>()
                .ForMember(dest => dest.Id, _ => _.MapFrom(src => src.Id))
                .ForMember(dest => dest.Key, _ => _.MapFrom(src => src.Key))
                .ForMember(dest => dest.Value, _ => _.MapFrom(src => src.Value))
                .ForMember(dest => dest.ProductId, _ => _.MapFrom(src => src.ProductId));
        }
    }
}
