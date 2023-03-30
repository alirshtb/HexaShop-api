using HexaShop.Application.Dtos.CategoryDtos.Commands;
using HexaShop.Application.Dtos.CategoryDtos.Queries;
using HexaShop.Common.CommonDtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexaShop.Application.Features.CategoryFeatures.Requests.Commands
{
    public class EditCategoryCR : IRequest<ResultDto<int>>
    {
        public EditCategoryDto EditCategoryDto { get; set; }
    }
}
