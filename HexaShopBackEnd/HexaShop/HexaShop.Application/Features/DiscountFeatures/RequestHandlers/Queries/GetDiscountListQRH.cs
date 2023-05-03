using AutoMapper;
using HexaShop.Application.Constracts.PersistanceContracts;
using HexaShop.Application.Dtos.DiscountDtos;
using HexaShop.Application.Features.DiscountFeatures.Requests.Queries;
using HexaShop.Common;
using HexaShop.Common.Dtos;
using HexaShop.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexaShop.Application.Features.DiscountFeatures.RequestHandlers.Queries
{
    public class GetDiscountListQRH : IRequestHandler<GetDiscountListQR, GetListResultDto<DiscountDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetDiscountListQRH(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<GetListResultDto<DiscountDto>> Handle(GetDiscountListQR request, CancellationToken cancellationToken)
        {
            var includes = new List<string>()
            {
                "Products"
            };

            var allDiscounts = _unitOfWork.DiscountRepository.GetAllAsQueryable(includes);

            #region Filters

            if (!string.IsNullOrWhiteSpace(request.GetDiscountListRequest.Search))
            {
                allDiscounts = allDiscounts.Where(d => d.Title.ToLower().Contains(request.GetDiscountListRequest.Search) ||
                                                       d.Description.ToLower().Contains(request.GetDiscountListRequest.Search));
            }

            if (request.GetDiscountListRequest.Percent.HasValue)
            {
                allDiscounts = allDiscounts.Where(d => d.Percent == request.GetDiscountListRequest.Percent);
            }


            #endregion

            #region Ordering

            allDiscounts = allDiscounts.SystemOrderBy(request.GetDiscountListRequest.OrderBy, request.GetDiscountListRequest.OrderDirection);

            #endregion

            var pagedList = PagedList<Discount>.Create(source: allDiscounts,
                                                       pageNumber: request.GetDiscountListRequest.PageNumber,
                                                       pageSize: request.GetDiscountListRequest.PageSize,
                                                       search: request.GetDiscountListRequest.Search);


            var values = _mapper.Map<IEnumerable<DiscountDto>>(pagedList);

            return new GetListResultDto<DiscountDto>()
            {
                MetaData = pagedList.MetaData,
                Values = values
            };


        }
    }
}
