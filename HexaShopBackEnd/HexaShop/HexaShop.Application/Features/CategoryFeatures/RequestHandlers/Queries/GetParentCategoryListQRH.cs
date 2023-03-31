using AutoMapper;
using HexaShop.Application.Constracts.PersistanceContracts;
using HexaShop.Application.Dtos.CategoryDtos.Queries;
using HexaShop.Application.Features.CategoryFeatures.Requests.Queries;
using HexaShop.Common;
using HexaShop.Common.CommonExtenstionMethods;
using HexaShop.Common.Dtos;
using HexaShop.Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Routing;

namespace HexaShop.Application.Features.CategoryFeatures.RequestHandlers.Queries
{
    public class GetParentCategoryListQRH : IRequestHandler<GetParentCategoryListQR, GetListResultDto<GetParentCategoryListDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetParentCategoryListQRH(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        /// <summary>
        /// get parent category list.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<GetListResultDto<GetParentCategoryListDto>> Handle(GetParentCategoryListQR request, CancellationToken cancellationToken)
        {
            var includes = new List<string>()
            {
                "ChildCategories"
            };

            var parentCategories = _unitOfWork.CategoryRepository.GetParents(includes: includes);

            if (!string.IsNullOrWhiteSpace(request.GetCategoryList.Search))
            {
                parentCategories = parentCategories.Where(pc => pc.Name.ToLower().Contains(request.GetCategoryList.Search) ||
                                                                pc.Description.ToLower().Contains(request.GetCategoryList.Search));
            }

            // TODO : ordering ...

            var paginatedList = PagedList<Category>.Create(parentCategories,
                                                           pageSize: request.GetCategoryList.PageSize,
                                                           pageNumber: request.GetCategoryList.PageNumber,
                                                           search: request.GetCategoryList.Search);

            var values = _mapper.Map<IEnumerable<GetParentCategoryListDto>>(paginatedList);

            var result = new GetListResultDto<GetParentCategoryListDto>()
            {
                Values = values,
                MetaData = paginatedList.MetaData
            };
            return result;
        }

    }
}
