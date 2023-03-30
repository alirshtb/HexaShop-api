using AutoMapper;
using HexaShop.Application.Constracts.PersistanceContracts;
using HexaShop.Application.Dtos.ProductDtos.Commands;
using HexaShop.Common.Exceptions;
using HexaShop.Common;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HexaShop.Application.Features.ProductFeatures.Requests.Commands;
using HexaShop.Common.CommonExtenstionMethods;
using HexaShop.Application.Dtos.ProductDtos.Queries;
using HexaShop.Application.Features.ProductFeatures.Requests.Queries;
using HexaShop.EndPoint.Models.ViewModels.ProductController;

namespace HexaShop.EndPoint.Controllers
{
    [Route("api/[controller]/")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public ProductController(IUnitOfWork unitOfWork,
                                 IMapper mapper, 
                                 IMediator mediator)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _mediator = mediator;
        }


        /// <summary>
        /// Create new Product.
        /// </summary>
        /// <param name="createProductViewModel"></param>
        /// <returns></returns>
        /// <exception cref="BadRequestException"></exception>
        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] CreateProductDto createProductDto)
        {
            try
            {

                var createProductCommandRequest = new CreateProductCR()
                {
                    CreateProductDto = createProductDto
                };

                var createProductResult = await _mediator.Send(createProductCommandRequest);

                createProductResult.ThrowException<int>();

                return Ok(createProductResult.ResultData);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        /// <summary>
        /// get prduct by id.
        /// </summary>
        /// <param name="id">int id</param>
        /// <returns>productDto</returns>
        [HttpGet("Get/{id}")]
        public async Task<ActionResult<GetProductViewModel>> Get(int id)
        {
            try
            {
                var productDto = await _mediator.Send(new GetProductQR()
                {
                    ProductId = id
                });

                var result = _mapper.Map<GetProductViewModel>(productDto);

                return Ok(result);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
