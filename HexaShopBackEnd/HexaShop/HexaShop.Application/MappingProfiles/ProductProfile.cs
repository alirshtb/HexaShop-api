using AutoMapper;
using HexaShop.Application.Dtos.ProductDtos.Commands;
using HexaShop.Application.Dtos.ProductDtos.Queries;
using HexaShop.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexaShop.Application.MappingProfiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<CreateProductDto, Product>()
                .ForMember(dest => dest.Images, _ => _.MapFrom(src => src.Images))
                .ForMember(dest => dest.Details, _ => _.MapFrom(src => src.Details))
                .ForMember(dest => dest.Title, _ => _.MapFrom(src => src.Title))
                .ForMember(dest => dest.Description, _ => _.MapFrom(src => src.Description))
                .ForMember(dest => dest.MainImage, _ => _.MapFrom(src => src.MainImage)) // --- first off must be uploaded and map to product model --- //
                .ForMember(dest => dest.Price, _ => _.MapFrom(src => src.Price))
                .ForMember(dest => dest.Score, _ => _.MapFrom(src => src.Score))
                .ForMember(dest => dest.Categories, _ => _.Ignore());


            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.Id, _ => _.MapFrom(src => src.Id))
                .ForMember(dest => dest.Images, _ => _.MapFrom(src => src.Images))
                .ForMember(dest => dest.Details, _ => _.MapFrom(src => src.Details));

            CreateMap<Product, GetProductListDto>()
                .ForMember(dest => dest.ImagesCount, _ => _.MapFrom(src => src.Images.Count()))
                .ForMember(dest => dest.DetailsCount, _ => _.MapFrom(src => src.Details.Count()))
                .ForMember(dest => dest.CategoriesCount, _ => _.MapFrom(src => src.Categories.Count()))
                .ForMember(dest => dest.MainImgae, _ => _.MapFrom(src => src.MainImage));
        }
    }
}
