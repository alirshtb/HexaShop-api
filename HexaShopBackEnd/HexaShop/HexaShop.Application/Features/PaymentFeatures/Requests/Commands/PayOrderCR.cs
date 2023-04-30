using HexaShop.Common.CommonDtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexaShop.Application.Features.PaymentFeatures.Requests.Commands
{
    public class PayOrderCR : IRequest<ResultDto<int>>
    {
        public int OrderId { get; set; }
    }
}
