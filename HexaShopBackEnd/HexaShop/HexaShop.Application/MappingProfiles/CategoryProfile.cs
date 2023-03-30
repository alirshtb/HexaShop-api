using AutoMapper;
using HexaShop.Application.Dtos.CategoryDtos.Commands;
using HexaShop.Application.Dtos.CategoryDtos.Queries;
using HexaShop.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexaShop.Application.MappingProfiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<CreateCategoryDto, Category>();
            CreateMap<Category, CategoryDto>()
                .ForMember(dest => dest.ParentId, _ => _.MapFrom(src => src.ParentCategoryId))
                .ForMember(dest => dest.Childs, _ => _.MapFrom(src => src.ChildCategories));

            CreateMap<EditCategoryDto, Category>();

        }
    }
}
