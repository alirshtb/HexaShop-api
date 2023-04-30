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

                // --- created order --- //
                var orderId = await _mediator.Send(new CreateOrderCR()
                {
                    CartId = cartId,
                    //AppUserId = (int)currentUserId
                    AppUserId = 3
                });

                // --- pay order --- //
                var paymentId = await _mediator.Send(new PayOrderCR()
                {
                    OrderId = orderId.ResultData
                });

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
