using AutoMapper;
using HexaShop.Application.Dtos.DiscountDtos;
using HexaShop.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexaShop.Application.MappingProfiles
{
    public class DiscountProfile : Profile
    {
        public DiscountProfile()
        {
            CreateMap<EditDiscountDto, Discount>();

            CreateMap<Discount, DiscountDto>()
                .ForMember(dest => dest.Title, _ => _.MapFrom(src => src.Title))
                .ForMember(dest => dest.Description, _ => _.MapFrom(src => src.Description))
                .ForMember(dest => dest.Products, _ => _.MapFrom(src => src.Products))
                .ForMember(dest => dest.Id, _ => _.MapFrom(src => src.Id));
        }
    }
}
