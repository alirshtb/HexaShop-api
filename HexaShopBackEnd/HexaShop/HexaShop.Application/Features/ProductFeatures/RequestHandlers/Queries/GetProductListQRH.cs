using AutoMapper;
using HexaShop.Application.Constracts.PersistanceContracts;
using HexaShop.Application.Dtos.DiscountDtos;
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
    public class GetProductListQRH : IRequestHandler<GetProductListQR, GetListResultDto<GetProductListDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetProductListQRH(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<GetListResultDto<GetProductListDto>> Handle(GetProductListQR request, CancellationToken cancellationToken)
        {
            var includes = new List<string>()
            {
                "Details",
                "Images",
                "Categories"
            };

            var products = _unitOfWork.ProductRepository.GetQueryableProducts(includes);

            #region apply filters 

            if (!string.IsNullOrWhiteSpace(request.GetProductListRequest.Search))
            {
                products = products.Where(p => p.Title.ToLower().Contains(request.GetProductListRequest.Search) || 
                                               p.Description.ToLower().Contains(request.GetProductListRequest.Search));
            }

            if(request.GetProductListRequest.IsSpecial.HasValue)
            {
                products = products.Where(p => p.IsSpecial == request.GetProductListRequest.IsSpecial.Value);
            }

            if(request.GetProductListRequest.Score.HasValue)
            {
                products = products.Where(p => p.Score == request.GetProductListRequest.Score.Value);
            }

            if(request.GetProductListRequest.Price.HasValue)
            {
                products = products.Where(p => p.Price == request.GetProductListRequest.Price.Value);
            }

            #endregion apply filters


            #region Apply Ordering

            products = products.SystemOrderBy(request.GetProductListRequest.OrderBy, request.GetProductListRequest.OrderDirection);

            #endregion apply ordering


            var paginatedList = PagedList<Product>.Create(source: products,
                                                          pageNumber: request.GetProductListRequest.PageNumber,
                                                          pageSize: request.GetProductListRequest.PageSize,
                                                          search: request.GetProductListRequest.Search);

            var values = _mapper.Map<IEnumerable<GetProductListDto>>(paginatedList);

            return new GetListResultDto<GetProductListDto>()
            {
                MetaData = paginatedList.MetaData,
                Values = values
            };
        }
    }
}
