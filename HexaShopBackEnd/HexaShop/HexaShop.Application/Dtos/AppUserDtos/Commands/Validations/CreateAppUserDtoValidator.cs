using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexaShop.Application.Dtos.AppUserDtos.Commands.Validations
{
    public class CreateAppUserDtoValidator : AbstractValidator<CreateAppUserDto>
    {
        public CreateAppUserDtoValidator()
        {
            RuleFor(_ => _.FirstName)
                .NotEmpty().WithMessage("{PropertyName} is Required.")
                .NotNull().WithMessage("{PropertyName} is Required.")
                .MaximumLength(50).WithMessage("{PropertyName} most not exced 50 characters.");

            RuleFor(_ => _.LastName)
               .NotEmpty().WithMessage("{PropertyName} is Required.")
               .NotNull().WithMessage("{PropertyName} is Required.")
               .MaximumLength(50).WithMessage("{PropertyName} most not exced 50 characters.");

            RuleFor(_ => _.Email)
               .NotEmpty().WithMessage("{PropertyName} is Required.")
               .NotNull().WithMessage("{PropertyName} is Required.")
               .MaximumLength(50).WithMessage("{PropertyName} most not exced 50 characters.");

            RuleFor(_ => _.Mobile)
               .NotEmpty().WithMessage("{PropertyName} is Required.")
               .NotNull().WithMessage("{PropertyName} is Required.")
               .MaximumLength(50).WithMessage("{PropertyName} most not exced 50 characters.");

            RuleFor(_ => _.Gender)
               .NotEmpty().WithMessage("{PropertyName} is Required.")
               .NotNull().WithMessage("{PropertyName} is Required.");
        }
    }
}
