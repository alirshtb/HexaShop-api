using HexaShop.Application.Dtos.ProductDtos.Commands;
using HexaShop.Common.CommonDtos;
using MediatR;

namespace HexaShop.Application.Features.ProductFeatures.Requests.Commands
{
    public class EditProductCR : IRequest<ResultDto<int>>
    {
        public EditProductDto EditProductData { get; set; }
    }
}
