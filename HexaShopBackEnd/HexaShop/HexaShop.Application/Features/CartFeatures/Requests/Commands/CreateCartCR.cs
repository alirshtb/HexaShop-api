using HexaShop.Common.CommonDtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexaShop.Application.Features.CartFeatures.Requests.Commands
{
    public class CreateCartCR : IRequest<ResultDto<int>>
    {
        public Guid BrowserId { get; set; }
        public int? UserId { get; set; }
    }
}
