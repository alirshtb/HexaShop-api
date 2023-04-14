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
        }
    }
}
