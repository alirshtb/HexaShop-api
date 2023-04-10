using AutoMapper;
using HexaShop.Application.Constracts.PersistanceContracts;
using HexaShop.Application.Features.CartFeatures.Requests.Commands;
using HexaShop.Application.Features.CartFeatures.Requests.Validations;
using HexaShop.Common;
using HexaShop.Common.CommonDtos;
using HexaShop.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexaShop.Application.Features.CartFeatures.RequestHandlers.Commands
{
    public class AddProductToCartCRH : IRequestHandler<AddProductToCartCR, ResultDto<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AddProductToCartCRH(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        /// <summary>
        /// Add a product to user cart.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<ResultDto<int>> Handle(AddProductToCartCR request, CancellationToken cancellationToken)
        {
            var validator = new AddProductToCartCRValidator();

            var validationResult = validator.Validate(request);

            if(!validationResult.IsValid)
            {
                return new ResultDto<int>()
                {
                    IsSuccess = false,
                    Message = validationResult.Errors.FirstOrDefault().ToString(),
                    Reason = Common.FailureReason.InvalidModel,
                    ResultData = 0
                };
            }


            // --- get cart --- //
            // --- find cart with userId if is not null or with browserId --- //
            var cartIncludes = new List<string>()
            {
                "User",
                "Items"
            };
            var cart = request.UserId.HasValue ? _unitOfWork.CartRepository.GetActiveByUserId(request.UserId.Value, includes: cartIncludes) :
                                                 _unitOfWork.CartRepository.GetActiveByBrowserId(request.BrowserId, includes: cartIncludes);

            // --- create new cart if cart is null --- //
            if(cart == null)
            {
                cart = new Cart()
                {
                    AppUserId = request.UserId == null ? null : request.UserId.Value,
                    BrowserId = new Guid(request.BrowserId),
                    IsFinished = false,
                    IsActive = true,
                    IsDeleted = false
                };
                _unitOfWork.CartRepository.Add(cart);
            }

            if(request.UserId.HasValue && cart.AppUserId == null)
            {
                cart.AppUserId = request.UserId.Value;
            }

            // --- add items to cart --- //
            var product = await _unitOfWork.ProductRepository.GetAsync(request.ProductId);
            if(product == null)
            {
                return new ResultDto<int>()
                {
                    IsSuccess = false,
                    Message = ApplicationMessages.ProductNotFound,
                    Reason = FailureReason.NotFound,
                    ResultData = 0
                };
            }

            cart = await _unitOfWork.CartRepository.GetAsync(cart.Id, includes: cartIncludes);

            cart.Items.Add(new CartItems()
            {
                Count = request.Count,
                Price = request.Price,
                IsActive = true,
                ProductId = product.Id
            });
            
            _unitOfWork.CartRepository.Update(cart);

            return new ResultDto<int>()
            {
                IsSuccess = true,
                Message = ApplicationMessages.ProductAddedToCart,
                ResultData = cart.Id
            };
        }
    }
}
