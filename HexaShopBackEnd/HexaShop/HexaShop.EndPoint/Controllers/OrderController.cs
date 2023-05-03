using AutoMapper;
using HexaShop.Application.Constracts.PersistanceContracts;
using HexaShop.Application.Features.OrderFeatures.Requests.Commands;
using HexaShop.Application.Features.PaymentFeatures.Requests.Commands;
using HexaShop.Common;
using HexaShop.Common.CommonExtenstionMethods;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using System.Net.NetworkInformation;

namespace HexaShop.EndPoint.Controllers
{
    [Route("api/[controller]/")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public OrderController(IMediator mediator, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mediator = mediator;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// create order.
        /// </summary>
        /// <param name="cartId"></param>
        /// <returns></returns>
        [HttpPost("Create")]
        public async Task<IActionResult> Order(int cartId)
        {
            try
            {

                var currentUserId = await _unitOfWork.AppUserRepository.GetCurrentUserId(User);

                // --- no user is signed in --- //
                if (currentUserId == null)
                {
                    ExceptionHelpers.ThrowException(ApplicationMessages.NoSignedInUserFound);
                }

                // --- first check internet connection --- //
                if (!CommonStaticFunctions.CheckInternetConnection())
                {
                    ExceptionHelpers.ThrowException(ApplicationMessages.ThereIsNotInternetConnection);
                }

                
                // --- created order --- //
                var createOrderResult = await _mediator.Send(new CreateOrderCR()
                {
                    CartId = cartId,
                    AppUserId = (int)currentUserId
                });


                // --- find order --- //
                var orderIncludes = new List<string>()
                {
                    "Payments",
                    "Cart.Items"
                };
                var order = await _unitOfWork.OrderRepository.GetAsync(createOrderResult.ResultData, includes: orderIncludes);

                if(order == null)
                {
                    ExceptionHelpers.ThrowException(ApplicationMessages.OrderNotFound);
                }

                
                // --- pay order --- //
                var paymentResult = await _mediator.Send(new PayOrderCR()
                {
                    OrderId = createOrderResult.ResultData
                });

                using var transaction = await _unitOfWork.BeginTransactionAsync();

                // --- update order if payment is successful --- //
                if (order.Payments.Any(p => p.IsSuccessful))
                {
                    order.IsCompleted = true;
                    await _unitOfWork.OrderRepository.UpdateAsync(order);

                    await _unitOfWork.CartRepository.ClearItems(order.CartId);

                }

                await transaction.CommitAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// confirm order.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut("{id}/Confirm")]
        public async Task<IActionResult> Confirm(int id)
        {
            try
            {
                var includes = new List<string>()
                {
                    "LevelLogs"
                };
                var order = await _unitOfWork.OrderRepository.GetAsync(id, includes: includes);
                if(order is null)
                {
                    ExceptionHelpers.ThrowException(ApplicationMessages.OrderNotFound);
                }

                // --- validate order status --- //
                if(order.Level != OrderProgressLevel.Paid)
                {
                    ExceptionHelpers.ThrowException(ApplicationMessages.OrderLevelIsNotProperToConfirm);
                }

                // --- confirm --- //
                await _unitOfWork.OrderRepository.ChagneOrderLevel(order.Id, OrderProgressLevel.Delivery, title: ApplicationMessages.OrderConfirmed);

                return Ok(order.Id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// sent order to user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut("{id}/SendToDestination")]
        public async Task<IActionResult> SendToDestination(int id)
        {
            try
            {
                var includes = new List<string>()
                {
                    "LevelLogs"
                };
                var order = await _unitOfWork.OrderRepository.GetAsync(id, includes: includes);
                if (order is null)
                {
                    ExceptionHelpers.ThrowException(ApplicationMessages.OrderNotFound);
                }

                // --- validate order status --- //
                if (order.Level != OrderProgressLevel.Confirmed)
                {
                    ExceptionHelpers.ThrowException(ApplicationMessages.OrderLevelIsNotProperToDelivery);
                }

                // --- confirm --- //
                await _unitOfWork.OrderRepository.ChagneOrderLevel(order.Id, OrderProgressLevel.RecievedByCustomer, title: ApplicationMessages.OrderSentToUser);

                return Ok(order.Id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// reject 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut("{id}/Reject")]
        public async Task<IActionResult> Reject(int id)
        {
            try
            {
                var includes = new List<string>()
                {
                    "LevelLogs"
                };
                var order = await _unitOfWork.OrderRepository.GetAsync(id, includes: includes);
                if (order is null)
                {
                    ExceptionHelpers.ThrowException(ApplicationMessages.OrderNotFound);
                }

                // --- validate order status --- //
                if (order.Level <= OrderProgressLevel.Confirmed)
                {
                    ExceptionHelpers.ThrowException(ApplicationMessages.OrderIsInNotProperLevelToReject);
                }

                // ToDo : if payment realy is successful must be returned.
                // code ...

                // --- confirm --- //
                await _unitOfWork.OrderRepository.ChagneOrderLevel(order.Id, OrderProgressLevel.Rejected, title: ApplicationMessages.OrderRejected);

                return Ok(order.Id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



    }
}
