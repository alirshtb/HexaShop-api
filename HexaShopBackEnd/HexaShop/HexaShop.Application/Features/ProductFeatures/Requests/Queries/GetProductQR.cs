using HexaShop.Application.Dtos.ProductDtos.Queries;
using MediatR;


namespace HexaShop.Application.Features.ProductFeatures.Requests.Queries
{
    public class GetProductQR : IRequest<ProductDto>
    {
        public int ProductId { get; set; }
    }
}
