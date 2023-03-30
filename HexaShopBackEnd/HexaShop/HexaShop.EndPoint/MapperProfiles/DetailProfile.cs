using AutoMapper;
using HexaShop.Application.Dtos.DetailDtos.Queries;
using HexaShop.EndPoint.Models.ViewModels.ProductController;

namespace HexaShop.EndPoint.MapperProfiles
{
    public class DetailProfile : Profile
    {
        public DetailProfile()
        {
            CreateMap<DetailDto, GetDetailViewModel>();
        }
    }
}
