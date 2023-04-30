using FluentValidation;

namespace HexaShop.EndPoint.Models.ViewModels.ProductController.Validations
{
    public class AddProductToCartViewModelValidator : AbstractValidator<AddProductToCartViewModel>
    {
        public AddProductToCartViewModelValidator()
        {

            RuleFor(apc => apc.Count)
                .NotNull().WithMessage("{PropertyName} can't be null.")
                .NotEmpty().WithMessage("{PropertyName} can't be empty.")
                .GreaterThanOrEqualTo(0).WithMessage("{PropertyName} Must be Equal or Greater than 0.");

            RuleFor(apc => apc.ProductId)
                .NotNull().WithMessage("{PropertyName} can't be null.")
                .NotEmpty().WithMessage("{PropertyName} can't be empty.");
        }
    }
}
