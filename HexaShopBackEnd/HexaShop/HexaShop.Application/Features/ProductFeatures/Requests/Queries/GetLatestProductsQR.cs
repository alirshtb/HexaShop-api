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
    public class GetLatestProductsQR : IRequest<GetListResultDto<GetProductToShowDto>>
    {
        public GetLatedProductListRequestDto GetProductListRequest { get; set; }
    }
}
