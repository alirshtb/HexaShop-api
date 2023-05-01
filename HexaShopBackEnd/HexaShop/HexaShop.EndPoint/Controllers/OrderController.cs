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


        [HttpPost("Create")]
        public async Task<IActionResult> Order(int cartId)
        {
            try
            {

                //var currentUserId = await _unitOfWork.AppUserRepository.GetCurrentUserId(User);

                // --- no user is signed in --- //
                //if(currentUserId == null)
                //{
                //    ExceptionHelpers.ThrowException(ApplicationMessages.NoSignedInUserFound);
                //}

                // --- first check internet connection --- //
                if(!CommonStaticFunctions.CheckInternetConnection())
                {
                    ExceptionHelpers.ThrowException(ApplicationMessages.ThereIsNotInternetConnection);
                }


                
                // --- created order --- //
                var createOrderResult = await _mediator.Send(new CreateOrderCR()
                {
                    CartId = cartId,
                    //AppUserId = (int)currentUserId
                    AppUserId = 3
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

    }
}
