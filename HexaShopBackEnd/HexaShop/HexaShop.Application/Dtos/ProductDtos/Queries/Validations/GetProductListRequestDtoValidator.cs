using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexaShop.Application.Dtos.ProductDtos.Queries.Validations
{
    public class GetProductListRequestDtoValidator : AbstractValidator<GetProductListRequestDto>
    {
        public GetProductListRequestDtoValidator()
        {
            
        }
    }
}
