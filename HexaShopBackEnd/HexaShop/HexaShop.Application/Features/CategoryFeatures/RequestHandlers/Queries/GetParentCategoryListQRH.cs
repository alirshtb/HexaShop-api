using AutoMapper;
using HexaShop.Application.Constracts.PersistanceContracts;
using HexaShop.Application.Dtos.CategoryDtos.Queries;
using HexaShop.Application.Features.CategoryFeatures.Requests.Queries;
using HexaShop.Common.CommonExtenstionMethods;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexaShop.Application.Features.CategoryFeatures.RequestHandlers.Queries
{
    public class GetParentCategoryListQRH : IRequestHandler<GetParentCategoryListQR, List<GetParentCategoryListDto>>
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
        public async Task<List<GetParentCategoryListDto>> Handle(GetParentCategoryListQR request, CancellationToken cancellationToken)
        {
            var parentCategories = _unitOfWork.CategoryRepository.GetParents(new List<string>()
            {
                "ChildCategories"
            });


            if(!string.IsNullOrWhiteSpace(request.GetCategoryList.Search))
            {
                parentCategories = (from category in parentCategories
                                    let search = request.GetCategoryList.Search.ToLower()
                                    let categoryName = category.Name.ToLower()
                                    let categoryDescription = category.Description.ToLower()
                                    where categoryName.StartsWith(search) ||
                                          categoryName.Contains(search) ||
                                          categoryDescription.StartsWith(search) ||
                                          categoryDescription.Contains(search)
                                    select category
                                    );
            }


            var paginatedList = parentCategories.AsEnumerable()
                                                .GetPaginatedList(request.GetCategoryList.PageNumber,
                                                                  request.GetCategoryList.PageSize)
                                                .ToList();

            var result = _mapper.Map<List<GetParentCategoryListDto>>(paginatedList);

            return result;
            
        }
    }
}
