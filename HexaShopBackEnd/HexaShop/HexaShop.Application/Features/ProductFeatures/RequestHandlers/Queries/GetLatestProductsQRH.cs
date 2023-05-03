using AutoMapper;
using HexaShop.Application.Constracts.PersistanceContracts;
using HexaShop.Application.Dtos.ProductDtos.Queries;
using HexaShop.Application.Features.ProductFeatures.Requests.Queries;
using HexaShop.Common;
using HexaShop.Common.Dtos;
using HexaShop.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexaShop.Application.Features.ProductFeatures.RequestHandlers.Queries
{
    public class GetLatestProductsQRH : IRequestHandler<GetLatestProductsQR, GetListResultDto<GetProductToShowDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetLatestProductsQRH(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<GetListResultDto<GetProductToShowDto>> Handle(GetLatestProductsQR request, CancellationToken cancellationToken)
        {
            var includes = new List<string>()
            {
                "Categories"
            };

            var products = _unitOfWork.ProductRepository.GetLatestActivesQueryable(includes);

            #region Apply Filters 

            if (!string.IsNullOrWhiteSpace(request.GetProductListRequest.Search))
            {
                products = products.Where(p => p.Title.ToLower().Contains(request.GetProductListRequest.Search) ||
                                               p.Description.ToLower().Contains(request.GetProductListRequest.Search));
            }

            if(request.GetProductListRequest.CategoryId.HasValue)
            {
                products = products.Where(p => p.Categories.Any(c => c.CategoryId == request.GetProductListRequest.CategoryId));
            }

            #endregion apply filters

            #region Apply Ordering

            products = products.SystemOrderBy(request.GetProductListRequest.OrderBy, request.GetProductListRequest.OrderDirection);

            #endregion apply ordering

            var paginatedList = PagedList<Product>.Create(source: products,
                                                          pageNumber: request.GetProductListRequest.PageNumber,
                                                          pageSize: request.GetProductListRequest.PageSize,
                                                          search: request.GetProductListRequest.Search);

            var values = _mapper.Map<IEnumerable<GetProductToShowDto>>(paginatedList);

            var result = new GetListResultDto<GetProductToShowDto>()
            {
                Values = values,
                MetaData = paginatedList.MetaData
            };

            return await Task.FromResult(result);
        }
    }
}
