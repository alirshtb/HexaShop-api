using FluentValidation;
using HexaShop.Application.Features.CartFeatures.Requests.Commands;

namespace HexaShop.Application.Features.CartFeatures.Requests.Validations
{
    public class AddProductToCartCRValidator : AbstractValidator<AddProductToCartCR>
    {
        public AddProductToCartCRValidator()
        {
            RuleFor(apc => apc.Price)
                .NotNull().WithMessage("{PropertyName} can't be null.")
                .NotEmpty().WithMessage("{PropertyName} can't be empty.")
                .GreaterThanOrEqualTo(0).WithMessage("{PropertyName} Must be Equal or Greater than 0.");

            RuleFor(apc => apc.Count)
                .NotNull().WithMessage("{PropertyName} can't be null.")
                .NotEmpty().WithMessage("{PropertyName} can't be empty.")
                .GreaterThanOrEqualTo(0).WithMessage("{PropertyName} Must be Equal or Greater than 0.");

            RuleFor(apc => apc.ProductId)
                .NotNull().WithMessage("{PropertyName} can't be null.")
                .NotEmpty().WithMessage("{PropertyName} can't be empty.");

            RuleFor(apc => apc.BrowserId)
                .NotNull().WithMessage("{PropertyName} can't be null.")
                .NotEmpty().WithMessage("{PropertyName} can't be empty.");

        }
    }
}
