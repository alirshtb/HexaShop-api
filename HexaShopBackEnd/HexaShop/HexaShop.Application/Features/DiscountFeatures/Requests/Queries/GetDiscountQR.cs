using HexaShop.Application.Dtos.DiscountDtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexaShop.Application.Features.DiscountFeatures.Requests.Queries
{
    public class GetDiscountQR : IRequest<DiscountDto>
    {
        public int DiscountId { get; set; }
    }
}
