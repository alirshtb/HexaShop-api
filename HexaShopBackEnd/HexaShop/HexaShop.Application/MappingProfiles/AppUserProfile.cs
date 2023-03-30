using AutoMapper;
using HexaShop.Application.Dtos.AppUserDtos.Commands;
using HexaShop.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexaShop.Application.MappingProfiles
{
    public class AppUserProfile : Profile
    {
        public AppUserProfile()
        {
            CreateMap<CreateAppUserDto, AppUser>();
        }
    }
}
