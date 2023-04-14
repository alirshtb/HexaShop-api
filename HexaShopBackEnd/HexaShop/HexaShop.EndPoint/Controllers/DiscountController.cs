using AutoMapper;
using HexaShop.Application.Constracts.PersistanceContracts;
using HexaShop.Application.Dtos.DiscountDtos;
using HexaShop.Application.Features.DiscountFeatures.Requests.Commands;
using HexaShop.Application.Features.DiscountFeatures.Requests.Queries;
using HexaShop.EndPoint.Models.ViewModels.DiscountController;
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
        private readonly IMapper _mapper;

        public DiscountController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
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

        /// <summary>
        /// edit discount.
        /// </summary>
        /// <param name="editDiscountDto"></param>
        /// <returns></returns>
        [HttpPost("Edit")]
        public async Task<IActionResult> Edit([FromBody] EditDiscountDto editDiscountDto)
        {
            try
            {
                var request = new EditDiscountCR()
                {
                    EditDiscountDto = editDiscountDto
                };

                var result = await _mediator.Send(request);

                return Ok(result.ResultData);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("Get/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var request = new GetDiscountQR()
                {
                    DiscountId = id
                };

                var result = await _mediator.Send(request);

                return Ok(_mapper.Map<DiscountViewModel>(result));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
