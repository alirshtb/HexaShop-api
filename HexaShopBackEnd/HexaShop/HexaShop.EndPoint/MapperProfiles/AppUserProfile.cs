using AutoMapper;
using HexaShop.Application.Dtos.AppUserDtos.Commands;
using HexaShop.EndPoint.Models.ViewModels.AccountController;

namespace HexaShop.EndPoint.MapperProfiles
{
    public class AppUserProfile : Profile
    {
        public AppUserProfile()
        {
            CreateMap<SignUpViewModel, CreateAppUserDto>();
        }
    }
}
