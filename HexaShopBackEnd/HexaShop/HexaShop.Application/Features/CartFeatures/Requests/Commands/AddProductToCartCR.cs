using HexaShop.Common.CommonDtos;
using MediatR; 

namespace HexaShop.Application.Features.CartFeatures.Requests.Commands
{
    public class AddProductToCartCR : IRequest<ResultDto<int>>
    {
        public string BrowserId { get; set; }
        public int? UserId { get; set; }
        public int ProductId { get; set; }
        public long Price { get; set; }
        public int Count { get; set; }
        public Microsoft.AspNetCore.Http.HttpContext HttpContext { get; set; }
    }
}
