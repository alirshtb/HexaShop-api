using FluentValidation;
using HexaShop.Application.Dtos.DetailDtos.Commands.Validations;
using HexaShop.Application.Dtos.ImageSourceDtos.Commands.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexaShop.Application.Dtos.ProductDtos.Commands.Validations
{
    public class CreateProductDtoValidator : AbstractValidator<CreateProductDto>
    {
        public CreateProductDtoValidator()
        {
            RuleFor(p => p.Title)
                .NotNull().WithMessage("{PropertyName} Cann't be Null.")
                .NotEmpty().WithMessage("{PropertyName} Cann't be Empty.")
                .MaximumLength(100).WithMessage("{PropertyName} Must not Exceed 100 characters.");

            RuleFor(p => p.Description)
                .NotNull().WithMessage("{PropertyName} Cann't be Null.")
                .NotEmpty().WithMessage("{PropertyName} Cann't be Empty.");

            RuleFor(p => p.Price)
                .NotNull().WithMessage("{PropertyName} Cann't be Null.")
                .NotEmpty().WithMessage("{PropertyName} Cann't be Empty.")
                .GreaterThanOrEqualTo(0).WithMessage("{PropertyName} Must be Equal or Greater than 0.");

            RuleFor(p => p.Score)
                .NotNull().WithMessage("{PropertyName} Cann't be Null.")
                .NotEmpty().WithMessage("{PropertyName} Cann't be Empty.")
                .GreaterThanOrEqualTo(0)
                .LessThanOrEqualTo(5);

            RuleFor(p => p.MainImage)
                .NotNull().WithMessage("{PropertyName} Cann't be Null.")
                .NotEmpty().WithMessage("{PropertyName} Cann't be Empty.");

            RuleFor(p => p.IsSpecial)
                .NotNull().WithMessage("{PropertyName} Cann't be Null.")
                .NotEmpty().WithMessage("{PropertyName} Cann't be Empty.");

            RuleFor(p => p.DiscountId)
                .NotNull().WithMessage("{PropertyName} Cann't be Null.")
                .NotEmpty().WithMessage("{PropertyName} Cann't be Empty."); ;

            RuleForEach(p => p.Images)
                .SetValidator(new CreateImageSourceDtoValidator());

            RuleForEach(p => p.Details)
                .SetValidator(new CreateDetailDtoValidator());

        }
    }
}
