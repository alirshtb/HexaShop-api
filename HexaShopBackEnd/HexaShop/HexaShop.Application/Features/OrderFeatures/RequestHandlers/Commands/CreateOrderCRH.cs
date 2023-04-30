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
                "Orders"
            };
            var cart = await _unitOfWork.CartRepository.GetAsync(request.CartId, includes: cartIncludes);

            if (cart == null)
            {
                ExceptionHelpers.ThrowException(ApplicationMessages.CartNotFound);
            }

            #region Delete order and order details before creating order

            var cartNotCompletedOrder = _unitOfWork.CartRepository.GetNotCompletedOrder(cart.Id);

            if(cartNotCompletedOrder != null)
            {
                // remove order --- //
                await DeleteOrderInfos(cartNotCompletedOrder.Id);
            }
            else if(cartNotCompletedOrder == null && cart.Orders.Count() > 0)
            {
                ExceptionHelpers.ThrowException(ApplicationMessages.UserHaveInProccessOrder);
            }

            #endregion Delete order and order details before creating order


            var cartItems = cart.Items;

            #region Calculations 

            // --- all count --- //
            int? count = cartItems.Sum(item => item.Count);

            // --- all amount --- // 
            long? amount = cartItems.Sum(item => item.Product.Price) * count;


            // --- all discount Percent --- //
            int? discountPercent = cartItems.Sum(item => item.Product.Discount.Percent);
            long allDiscountAmount = (long)cartItems.Sum(item => (item.Product.Price * item.Product.Discount.Percent / 100)) * (int)count;

            // --- amount afterDiscount --- //
            var netAmountAfterDiscount = amount - allDiscountAmount;

            // --- with tax --- //
            var taxAmount = Math.Ceiling((long)netAmountAfterDiscount / 1.09);
            var withTaxAmount = netAmountAfterDiscount + (Math.Floor(taxAmount * 9 / 100));

            #endregion Calculations

            // --- validate amount --- //
            if (withTaxAmount <= 0)
            {
                return new ResultDto<int>()
                {
                    IsSuccess = true,
                    ResultData = 0,
                    Message = "zero amount."
                };
            }

            // --- add order --- //
            using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                var order = new Order()
                {
                    Amount = (long)netAmountAfterDiscount,
                    AppUserId = request.AppUserId,
                    CartId = request.CartId,
                    DiscountAmount = allDiscountAmount,
                    IsCompleted = false,
                    IsActive = true,
                    DateCreated = DateTime.Now,
                    IsDeleted = false,
                    TaxAmount = (long)taxAmount,
                    Level = OrderProgressLevel.WaitToPayment,
                    LevelLogs = new List<OrderLevelLog>()
                    {
                        new OrderLevelLog() // --- log status --- //
                        {
                            CurrentLevel = OrderProgressLevel.WaitToPayment,
                            NextLevel = OrderProgressLevel.Paid,
                            Title = ApplicationMessages.OrderPaymentLevel
                        }
                    }
                };

                await _unitOfWork.OrderRepository.AddAsync(order);

                foreach (var item in cartItems)
                {
                    var orderDetail = new OrderDetails()
                    {
                        Count = item.Count,
                        UnitPrice = item.Price,
                        UnitDiscount = item.Price * item.Product.Discount.Percent / 100,
                        OrderId = order.Id,
                        ProductId = item.ProductId,
                        TotalDiscount = (item.Price * item.Product.Discount.Percent / 100) * item.Count,
                        TotalAmount = (item.Price * item.Count) // --- without deduction of discount --- //
                    };

                    await _unitOfWork.OrderDetailRepository.AddAsync(orderDetail);
                }

                await transaction.CommitAsync();

                // --- return result --- //
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

        /// <summary>
        /// delete order all info
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public async Task DeleteOrderInfos(int orderId)
        {
            var orderIncludes = new List<string>()
            {
                "Details",
                "LevelLogs",
                "Payments",
            };
            var order = await _unitOfWork.OrderRepository.GetAsync(orderId, orderIncludes);

            // --- delete order details --- //
            foreach (var orderDetail in order.Details)
            {
                await _unitOfWork.OrderDetailRepository.DeleteAsync(orderDetail.Id);
            }

            // --- delete order level logs --- //
            //order.LevelLogs.Clear();

            // --- delete order payments --- //
            foreach (var orderPayment in order.Payments)
            {
                await _unitOfWork.PaymentRepository.DeleteAsync(orderPayment.Id);
            }

            // --- delete order itself --- //
            await _unitOfWork.OrderRepository.DeleteAsync(order.Id);

            await _unitOfWork.SaveChangesAsync();

            await Task.CompletedTask;

        }


    }
}
