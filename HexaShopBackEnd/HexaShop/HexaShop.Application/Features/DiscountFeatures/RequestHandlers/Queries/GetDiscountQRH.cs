using AutoMapper;
using HexaShop.Application.Constracts.PersistanceContracts;
using HexaShop.Application.Dtos.DiscountDtos;
using HexaShop.Application.Features.DiscountFeatures.Requests.Queries;
using HexaShop.Common;
using HexaShop.Common.CommonExtenstionMethods;
using MediatR;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexaShop.Application.Features.DiscountFeatures.RequestHandlers.Queries
{
    public class GetDiscountQRH : IRequestHandler<GetDiscountQR, DiscountDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetDiscountQRH(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        /// <summary>
        /// get discount detail.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<DiscountDto> Handle(GetDiscountQR request, CancellationToken cancellationToken)
        {
            var discount = await _unitOfWork.DiscountRepository.GetAsync(request.DiscountId, includes: new List<string>()
            {
                "Products"
            });
            if(discount == null)
            {
                ExceptionHelpers.ThrowException(ApplicationMessages.DiscountNotFound);
            }

            var result = _mapper.Map<DiscountDto>(discount);

            return result;

        }
    }
}
