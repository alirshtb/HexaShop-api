using HexaShop.Application.Dtos.CategoryDtos.Queries;
using MediatR;

namespace HexaShop.Application.Features.CategoryFeatures.Requests.Queries
{
    public class GetParentCategoryListQR : IRequest<List<GetParentCategoryListDto>>
    {
        public GetCategoryListRequest GetCategoryList { get; set; }
    }
}
