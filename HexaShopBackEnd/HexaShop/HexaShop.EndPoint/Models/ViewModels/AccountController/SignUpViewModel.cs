using HexaShop.Application.Dtos.AppUserDtos.Commands;
using HexaShop.Common;
using System.ComponentModel.DataAnnotations;
using System.Security.Permissions;

namespace HexaShop.EndPoint.Models.ViewModels.AccountController
{
    public class SignUpViewModel : CreateAppUserDto
    {

        [Required(ErrorMessage = "رمز نمیتواند خالی باشد")]
        public string Password { get; set; }

        [Required(ErrorMessage = "تایید رمز نمیتواند خالی باشد")]
        [Compare("Password", ErrorMessage = "رمز و تایید یکسان نیست.")]
        public string ConfirmPassword { get; set; }

    }
}
