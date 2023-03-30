using AutoMapper;
using HexaShop.Application.Dtos.CategoryDtos.Queries;
using HexaShop.EndPoint.Models.ViewModels.CategoryController;

namespace HexaShop.EndPoint.MapperProfiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<CategoryDto, GetCategoryViewModel>()
                //.ForMember(dest => dest.ParentId, _ => _.MapFrom(src => src.ParentId))
                .ForMember(dest => dest.Childs, _ => _.MapFrom(src => src.Childs));
        }
    }
}
