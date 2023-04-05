using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace HexaShop.Application.Dtos.CategoryDtos.Commands.Validations
{
    public class CreateCategoryDtoValidator : AbstractValidator<CreateCategoryDto>
    {
        public CreateCategoryDtoValidator()
        {
            RuleFor(ccd => ccd.Name)
                .NotNull().WithMessage("{PropertyName} can't be null.")
                .NotEmpty().WithMessage("{PropertyName} can't be empty.")
                .MaximumLength(30).WithMessage("{PropertyName} must not exceed 30 characters.")
                .MinimumLength(3).WithMessage("{PropertyName} must larger than 3 characters.");

            RuleFor(ccd => ccd.Description)
                .NotNull().WithMessage("{PropertyName} can't be null.")
                .NotEmpty().WithMessage("{PropertyName} can't be empty.")
                .MaximumLength(200).WithMessage("{PropertyName} must not exceed 200 characters.")
                .MinimumLength(10).WithMessage("{PropertyName} must larger than 10 characters.");

            RuleFor(ccd => ccd.Image)
                .NotNull().WithMessage("{PropertyName} can't be null.")
                .NotEmpty().WithMessage("{PropertyName} can't be empty.");

        }
    }
}
