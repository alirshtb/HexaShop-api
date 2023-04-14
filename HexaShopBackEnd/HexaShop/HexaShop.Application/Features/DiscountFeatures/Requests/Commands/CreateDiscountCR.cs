using HexaShop.Application.Dtos.DiscountDtos;
using HexaShop.Common.CommonDtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexaShop.Application.Features.DiscountFeatures.Requests.Commands
{
    public class CreateDiscountCR : IRequest<ResultDto<int>>
    {
        public CreateDiscountDto CreateDiscountDto { get; set; }
    }
}
