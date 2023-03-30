using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexaShop.Application.Dtos.DetailDtos.Commands.Validations
{
    public class CreateDetailDtoValidator : AbstractValidator<CreateDetailDto>
    {
        public CreateDetailDtoValidator()
        {
            RuleFor(d => d.Key)
                .NotNull().WithMessage("{PropertyName} Cann't be Null.")
                .NotEmpty().WithMessage("{PropertyName} Cann't be Empty.")
                .MaximumLength(100).WithMessage("{PropertyName} Must not Exceed 100 characters.");

            RuleFor(d => d.Value)
                .NotNull().WithMessage("{PropertyName} Cann't be Null.")
                .NotEmpty().WithMessage("{PropertyName} Cann't be Empty.");

        }
    }
}
