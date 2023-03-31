using HexaShop.Application.Dtos.ProductDtos.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexaShop.Application.Features.ProductFeatures.Requests.Queries
{
    public class GetProductListQR : IRequest<List<GetProductListDto>>
    {
        public GetProductListRequest GetProductListRequest { get; set; }
    }
}
