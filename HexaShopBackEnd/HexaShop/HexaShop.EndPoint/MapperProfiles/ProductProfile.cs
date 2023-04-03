using AutoMapper;
using HexaShop.Application.Dtos.ProductDtos.Queries;
using HexaShop.EndPoint.Models.ViewModels.ProductController;

namespace HexaShop.EndPoint.MapperProfiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<ProductDto, GetProductViewModel>()
                .ForMember(dest => dest.Images, _ => _.MapFrom(src => src.Images))
                .ForMember(dest => dest.Details, _ => _.MapFrom(src => src.Details));

            CreateMap<GetProductToShowDto, GetProductToShowViewModel>();
        }
    }
}
