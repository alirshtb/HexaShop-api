using AutoMapper;
using HexaShop.Application.Dtos.DiscountDtos;
using HexaShop.EndPoint.Models.ViewModels.DiscountController;

namespace HexaShop.EndPoint.MapperProfiles
{
    public class DiscountProfile : Profile
    {
        public DiscountProfile()
        {
            CreateMap<DiscountDto, DiscountViewModel>()
                .ForMember(dest => dest.ProductsCount, _ => _.MapFrom(src => src.Products.Count()));
        }
    }
}
