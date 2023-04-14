using FluentValidation;

namespace HexaShop.Application.Dtos.DiscountDtos.Validations
{
    public class EditDiscountDtoValidator : AbstractValidator<EditDiscountDto>
    {
        public EditDiscountDtoValidator()
        {
            RuleFor(cd => cd.DiscountId)
                .NotNull().WithMessage("{PropertyName} Cann't be Null.")
                .NotEmpty().WithMessage("{PropertyName} Cann't be Empty.");

            RuleFor(cd => cd.Title)
                .NotNull().WithMessage("{PropertyName} Cann't be Null.")
                .NotEmpty().WithMessage("{PropertyName} Cann't be Empty.")
                .MaximumLength(20).WithMessage("{PropertyName} Must not Exceed 20 characters.");

            RuleFor(cd => cd.Description)
                .NotNull().WithMessage("{PropertyName} Cann't be Null.")
                .NotEmpty().WithMessage("{PropertyName} Cann't be Empty.")
                .MaximumLength(100).WithMessage("{PropertyName} Must not Exceed 100 characters."); ;

            RuleFor(cd => cd.Percent)
                .NotNull().WithMessage("{PropertyName} Cann't be Null.")
                .GreaterThanOrEqualTo(0).WithMessage("{PropertyName} Must equal or greater than 0.")
                .LessThanOrEqualTo(100).WithMessage("{PropertyName} Must equal or less than 100.");
        }
    }
}
