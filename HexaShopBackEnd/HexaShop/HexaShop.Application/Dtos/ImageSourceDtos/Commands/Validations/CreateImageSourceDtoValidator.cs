using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexaShop.Application.Dtos.ImageSourceDtos.Commands.Validations
{
    public class CreateImageSourceDtoValidator : AbstractValidator<CreateImageSourceDto>
    {
        public CreateImageSourceDtoValidator()
        {
            RuleFor(ims => ims.Name)
                .NotNull().WithMessage("{PropertyName} Cann't be Null.")
                .NotEmpty().WithMessage("{PropertyName} Cann't be Empty.")
                .MaximumLength(50).WithMessage("{PropertyName} Must not Exceed 50 characters.");

            RuleFor(ims => ims.Address)
                .NotNull().WithMessage("{PropertyName} Cann't be Null.")
                .NotEmpty().WithMessage("{PropertyName} Cann't be Empty.");
        }
    }
}
