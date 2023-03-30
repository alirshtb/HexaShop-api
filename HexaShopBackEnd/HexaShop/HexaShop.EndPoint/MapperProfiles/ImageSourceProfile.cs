using AutoMapper;
using HexaShop.Application.Dtos.ImageSourceDtos.Queries;
using HexaShop.EndPoint.Models.ViewModels.ProductController;

namespace HexaShop.EndPoint.MapperProfiles
{
    public class ImageSourceProfile : Profile
    {
        public ImageSourceProfile()
        {
            CreateMap<ImageSourceDto, GetImageSourceViewModel>();
        }
    }
}
