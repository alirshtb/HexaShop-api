using HexaShop.Application.Dtos.CategoryDtos.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexaShop.Application.Features.CategoryFeatures.Requests.Queries
{
    public class GetCategoryQR : IRequest<CategoryDto>
    {
        public int Id { get; set; }
    }
}
