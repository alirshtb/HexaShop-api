using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexaShop.Application.Dtos.Common.Validations
{
    public class GetListBaseDtoValidator : AbstractValidator<GetListBaseDto>
    {
        public GetListBaseDtoValidator()
        {
            RuleFor(bd => bd.PageNumber)
                .NotNull().WithMessage("{PropertyName} Cann't be Null.")
                .NotEmpty().WithMessage("{PropertyName} Cann't be Empty.");

            RuleFor(bd => bd.PageSize)
                .NotNull().WithMessage("{PropertyName} Cann't be Null.")
                .NotEmpty().WithMessage("{PropertyName} Cann't be Empty.");
        }
    }
}
