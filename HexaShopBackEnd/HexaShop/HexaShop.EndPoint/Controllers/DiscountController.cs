using HexaShop.Application.Constracts.PersistanceContracts;
using HexaShop.Application.Dtos.DiscountDtos;
using HexaShop.Application.Features.DiscountFeatures.Requests.Commands;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HexaShop.EndPoint.Controllers
{
    [Route("api/[controller]/")]
    [ApiController]
    public class DiscountController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DiscountController(IMediator mediator)
        {
            _mediator = mediator;
        }
        

        /// <summary>
        /// create new discount.
        /// </summary>
        /// <param name="createDiscountDto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create([FromBody] CreateDiscountDto createDiscountDto)
        {
            try
            {
                var request = new CreateDiscountCR()
                {
                    CreateDiscountDto = createDiscountDto
                };
                var result = await _mediator.Send(request);

                return Ok($"{result.Message} {result.ResultData}");

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
