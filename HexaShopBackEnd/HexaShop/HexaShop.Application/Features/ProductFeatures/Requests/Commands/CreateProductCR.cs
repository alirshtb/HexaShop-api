using HexaShop.Application.Dtos.ProductDtos.Commands;
using HexaShop.Common.CommonDtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexaShop.Application.Features.ProductFeatures.Requests.Commands
{
    public class CreateProductCR : IRequest<ResultDto<int>>
    {
        public CreateProductDto CreateProductDto { get; set; }
    }
}

