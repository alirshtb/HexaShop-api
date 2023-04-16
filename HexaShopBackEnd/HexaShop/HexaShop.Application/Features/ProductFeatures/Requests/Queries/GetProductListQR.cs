using HexaShop.Application.Dtos.DiscountDtos;
using HexaShop.Application.Dtos.ProductDtos.Queries;
using HexaShop.Common.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexaShop.Application.Features.ProductFeatures.Requests.Queries
{
    public class GetProductListQR : IRequest<GetListResultDto<GetProductListDto>>
    {
        public GetProductListRequestDto GetProductListRequest { get; set; }
    }
}
