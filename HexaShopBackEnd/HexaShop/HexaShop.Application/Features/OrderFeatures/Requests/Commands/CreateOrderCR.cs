using HexaShop.Common.CommonDtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexaShop.Application.Features.OrderFeatures.Requests.Commands
{
    public class CreateOrderCR : IRequest<ResultDto<int>>
    {
        public int CartId { get; set; }
        public int AppUserId { get; set; }
    }
}
