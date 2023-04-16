using HexaShop.Application.Dtos.DiscountDtos;
using HexaShop.Common.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexaShop.Application.Features.DiscountFeatures.Requests.Queries
{
    public class GetDiscountListQR : IRequest<GetListResultDto<DiscountDto>>
    {
        public GetDiscountListRequestDto GetDiscountListRequest { get; set; }
    }
}
