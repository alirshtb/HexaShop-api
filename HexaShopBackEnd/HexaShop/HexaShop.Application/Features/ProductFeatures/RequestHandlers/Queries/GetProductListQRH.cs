using AutoMapper;
using HexaShop.Application.Constracts.PersistanceContracts;
using HexaShop.Application.Dtos.ProductDtos.Queries;
using HexaShop.Application.Features.ProductFeatures.Requests.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexaShop.Application.Features.ProductFeatures.RequestHandlers.Queries
{
    public class GetProductListQRH : IRequestHandler<GetProductListQR, List<GetProductListDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetProductListQRH(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<GetProductListDto>> Handle(GetProductListQR request, CancellationToken cancellationToken)
        {
            var products = await _unitOfWork.ProductRepository.GetListAsync(includes: new List<string>()
            {
                "Details",
                "Images",
                "Categories"
            });

            //if (!string.IsNullOrWhiteSpace(request.GetProductListRequest.Search))
            //{
            //    products = (from product in products
            //                     let search = request.GetProductListRequest.Search.ToLower()
            //                     let productTitle = product.Title.ToLower()
            //                     let productDescription = product.Description.ToLower()
            //                     where productTitle.StartsWith(search) ||
            //                             productTitle.Contains(search) ||
            //                             productDescription.StartsWith(search) ||
            //                             productDescription.Contains(search)
            //                     select product
            //        ).AsEnumerable();
            //}

            return new List<GetProductListDto>();

        }
    }
}
