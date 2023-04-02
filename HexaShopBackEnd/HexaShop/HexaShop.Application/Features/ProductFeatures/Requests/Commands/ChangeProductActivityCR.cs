using HexaShop.Common.CommonDtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexaShop.Application.Features.ProductFeatures.Requests.Commands
{
    public class ChangeProductActivityCR : IRequest<ResultDto<int>>
    {
        public int ProductId { get; set; }
    }
}
