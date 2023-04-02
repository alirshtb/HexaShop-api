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
    public class EditProductDtoValidator : AbstractValidator<EditProductDto>
    {
        public EditProductDtoValidator()
        {
            RuleFor(pd => pd.ProductId)
                .NotNull().WithMessage("{PropertyName} cann't be null.")
                .NotEmpty().WithMessage("{PropertyName} cann't be empty.");

            RuleFor(pd => pd.Title)
                .NotNull().WithMessage("{PropertyName} cann't be null.")
                .NotEmpty().WithMessage("{PropertyName} cann't be empty.")
                .MaximumLength(100).WithMessage("{PropertyName} Must not Exceed 100 characters.");

            RuleFor(pd => pd.Description)
                .NotNull().WithMessage("{PropertyName} Cann't be Null.")
                .NotEmpty().WithMessage("{PropertyName} Cann't be Empty.");

            RuleFor(pd => pd.Price)
                .NotNull().WithMessage("{PropertyName} Cann't be Null.")
                .NotEmpty().WithMessage("{PropertyName} Cann't be Empty.")
                .GreaterThanOrEqualTo(0).WithMessage("{PropertyName} Must be Equal or Greater than 0.");

            RuleFor(pd => pd.MainImage)
                .NotNull().WithMessage("{PropertyName} Cann't be Null.")
                .NotEmpty().WithMessage("{PropertyName} Cann't be Empty.");

            RuleForEach(p => p.Images)
                .SetValidator(new CreateImageSourceDtoValidator());

            RuleForEach(p => p.Details)
                .SetValidator(new CreateDetailDtoValidator());
        }
    }
}
