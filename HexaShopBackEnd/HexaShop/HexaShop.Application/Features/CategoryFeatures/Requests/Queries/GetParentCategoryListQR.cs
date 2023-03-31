using HexaShop.Application.Dtos.CategoryDtos.Queries;
using HexaShop.Common.Dtos;
using HexaShop.Domain;
using MediatR;

namespace HexaShop.Application.Features.CategoryFeatures.Requests.Queries
{
    public class GetParentCategoryListQR : IRequest<GetListResultDto<GetParentCategoryListDto>>
    {
        public GetCategoryListRequestDto GetCategoryList { get; set; }
    }
}
