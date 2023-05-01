using HexaShop.Application.Constracts.PersistanceContracts;
using HexaShop.Application.Features.PaymentFeatures.Requests.Commands;
using HexaShop.Common;
using HexaShop.Common.CommonDtos;
using HexaShop.Common.CommonExtenstionMethods;
using HexaShop.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexaShop.Application.Features.PaymentFeatures.RequestHandlers.Commands
{
    public class PayOrderCRH : IRequestHandler<PayOrderCR, ResultDto<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
            
        public PayOrderCRH(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }

        /// <summary>
        /// pay order amount.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<ResultDto<int>> Handle(PayOrderCR request, CancellationToken cancellationToken)
        {
            var orderIncludes = new List<string>()
            {
                "Payments"
            };
            var order = await _unitOfWork.OrderRepository.GetAsync(request.OrderId, includes: orderIncludes);
            if(order == null)
            {
                ExceptionHelpers.ThrowException(ApplicationMessages.OrderNotFound);
            }

            // --- create payment with order amount --- //
            var payment = new ZarinpalSandbox.Payment((int)order.Amount);

            // --- send request to zarinPal sandbox --- //
            var payRequest = await payment.PaymentRequest($"order number {order.Id} payment.",
                                                   "https://localhost:44317/api/Product/Get/2",
                                                   "dev.alirashtbari@gmail.com",
                                                   "09917586411");

            try
            {
                using var transaction = await _unitOfWork.BeginTransactionAsync();
                // --- add new payment record --- //
                var orderPayment = new Payment()
                {
                    Amount = order.Amount,
                    Info = payRequest.Status.ToString(),
                    OrderId = order.Id,
                    IsSuccessful = true,
                    FailureReason = null,

                };

                order.Payments.Add(orderPayment);

                await _unitOfWork.OrderRepository.UpdateAsync(order);


                // --- if result is successful then change order level to wait to confirm --- //
                var payResult = payRequest;
                if (payResult.Status == 100)
                {
                    order.LevelLogs.Add(new OrderLevelLog()
                    {
                        CurrentLevel = order.LevelLogs.OrderByDescending(l => l.Id).First().NextLevel,
                        NextLevel = Common.OrderProgressLevel.Confirmed,
                        Title = ApplicationMessages.OrderPaidAndWaitToCinfirm,
                    });

                    await _unitOfWork.OrderRepository.UpdateAsync(order);
                }
                else // --- else chagne payment failure reason to payment status and payment is success to false --- //
                {
                    orderPayment.FailureReason = payRequest.Status.ToString();
                    orderPayment.IsSuccessful = false;

                    await _unitOfWork.PaymentRepository.UpdateAsync(orderPayment);
                }

                await transaction.CommitAsync();

                return new ResultDto<int>()
                {
                    IsSuccess = true,
                    Message = "order paid.",
                    ResultData = orderPayment.Id
                };
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollBackTransactionAsync();
                throw;
            }

        }
    }
}
