using System.ComponentModel.DataAnnotations;

namespace HexaShop.EndPoint.Models.ViewModels.AccountController
{
    public class SignInViewModel
    {
        [Required(ErrorMessage = "Enter the UserName.")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Enter the Password.")]
        public string Password { get; set; }
        public bool RememberMe { get; set; } = false;
    }
}
