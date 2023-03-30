using AutoMapper;
using HexaShop.Application.Dtos.ImageSourceDtos.Commands;
using HexaShop.Application.Dtos.ImageSourceDtos.Queries;
using HexaShop.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexaShop.Application.MappingProfiles
{
    public class ImageSourceProfile : Profile
    {
        public ImageSourceProfile()
        {
            CreateMap<CreateImageSourceDto, ImageSource>()
                .ForMember(dest => dest.Name, _ => _.MapFrom(src => src.Name))
                .ForMember(dest => dest.Address, _ => _.MapFrom(src => src.Address));

            CreateMap<ImageSource, ImageSourceDto>()
                .ForMember(dest => dest.Id, _ => _.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, _ => _.MapFrom(src => src.Name))
                .ForMember(dest => dest.Address, _ => _.MapFrom(src => src.Address))
                .ForMember(dest => dest.ProductId, _ => _.MapFrom(src => src.ProductId));

        }
    }
}
