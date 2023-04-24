using HexaShop.Application.Constracts.PersistanceContracts;
using HexaShop.Application.Features.OrderFeatures.Requests.Commands;
using HexaShop.Common;
using HexaShop.Common.CommonDtos;
using HexaShop.Common.CommonExtenstionMethods;
using HexaShop.Domain;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexaShop.Application.Features.OrderFeatures.RequestHandlers.Commands
{
    public class CreateOrderCRH : IRequestHandler<CreateOrderCR, ResultDto<int>>
    {

        private readonly IUnitOfWork _unitOfWork;

        public CreateOrderCRH(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// crate order if amount after discount is greater than zero.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<ResultDto<int>> Handle(CreateOrderCR request, CancellationToken cancellationToken)
        {
            var cartIncludes = new List<string>()
            {
                "Items",
                "Items.Product",
                "Items.Product.Discount",
            };
            var cart = await _unitOfWork.CartRepository.GetAsync(request.CartId, includes: cartIncludes);

            if (cart == null)
            {
                ExceptionHelpers.ThrowException(ApplicationMessages.CartNotFound);
            }


            var cartItems = cart.Items;

            // --- all amount --- // 
            long? amount = cartItems.Sum(item => item.Product.Price);

            // --- all count --- //
            int? count = cartItems.Sum(item => item.Count);

            // --- all discount Percent --- //
            int? discountPercent = cartItems.Sum(item => item.Product.Discount.Percent);
            var allDiscountAmount = cartItems.Sum(item => item.Product.Price - ((item.Product.Discount.Percent / 100) * item.Product.Price));

            // --- amount afterDiscount --- //
            var netAmountAfterDiscount = amount - allDiscountAmount;

            // --- with tax --- //
            var withTaxAmount = netAmountAfterDiscount + netAmountAfterDiscount * (9 / 100);


            if (withTaxAmount < 0)
            {
                return new ResultDto<int>()
                {
                    IsSuccess = true,
                    ResultData = 0,
                    Message = "zero amount."
                };
            }

            using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                var order = new Order()
                {
                    Amount = (long)withTaxAmount,
                    AppUserId = request.AppUserId,
                    CartId = request.CartId,
                    DiscountAmount = allDiscountAmount,
                    IsCompleted = false,
                    IsActive = true,
                    DateCreated = DateTime.Now,
                    IsDeleted = false,
                    Level = OrderProgressLevel.Payment,
                    LevelLogs = new List<OrderLevelLog>()
                    {
                        new OrderLevelLog()
                        {
                            CurrentLevel = OrderProgressLevel.Payment,
                            NextLevel = OrderProgressLevel.WaitToConfirm,
                            Title = ApplicationMessages.OrderPaymentLevel
                        }
                    }
                };

                await _unitOfWork.OrderRepository.AddAsync(order);

                await transaction.CommitAsync();

                return new ResultDto<int>()
                {
                    IsSuccess = true,
                    ResultData = order.Id,
                    Message = ApplicationMessages.OrderCreated
                };
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }

        }
    }
}
