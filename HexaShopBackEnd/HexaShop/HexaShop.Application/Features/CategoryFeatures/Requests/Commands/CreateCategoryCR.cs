using HexaShop.Application.Dtos.CategoryDtos.Commands;
using HexaShop.Common.CommonDtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexaShop.Application.Features.CategoryFeatures.Requests.Commands
{
    public class CreateCategoryCR : IRequest<ResultDto<int>>
    {
        public CreateCategoryDto CreateCategoryDto { get; set; }
    }
}
