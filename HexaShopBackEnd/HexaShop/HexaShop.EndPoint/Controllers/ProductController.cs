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
    [Route("api/[controller]/[action]/")]
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
        [HttpPost]
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

                return CreatedAtAction("Get", controllerName: "Product", routeValues: new { id = createProductResult.ResultData }, value: null);
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
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> Get(int id)
        {
            try
            {
                var productDto = await _mediator.Send(new GetProductQR()
                {
                    ProductId = id
                });

                return Ok(productDto);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// get product paginated list.
        /// </summary>
        /// <param name="getProductListRequest"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<IEnumerable<GetProductListDto>>> GetList([FromBody] GetProductListRequestDto getProductListRequest)
        {
            try
            {
                var request = new GetProductListQR()
                {
                     GetProductListRequest = getProductListRequest
                };
                var result = await _mediator.Send(request);

                Response.Headers.Add("X-Pagination", Newtonsoft.Json.JsonConvert.SerializeObject(result.MetaData));

                return Ok(result.Values);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// change product activity.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> ChangeActivity(int id)
        {
            try
            {
                var request = new ChangeProductActivityCR()
                {
                    ProductId = id
                };

                var result = await _mediator.Send(request);

                result.ThrowException<int>();

                return Ok(result.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Edit product.
        /// </summary>
        /// <param name="editProductDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Edit([FromBody] EditProductDto editProductDto)
        {
            try
            {
                var request = new EditProductCR()
                {
                    EditProductData = editProductDto
                };
                var result = await _mediator.Send(request);

                return Ok(result.Message + " " + result.ResultData);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// get product with less details to show on web site.
        /// </summary>
        /// <param name="getProductListRequest"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<IEnumerable<GetProductToShowViewModel>>> GetLatest([FromBody] GetLatedProductListRequestDto getProductListRequest)
        {
            try
            {
                var request = new GetLatestProductsQR()
                {
                    GetProductListRequest = getProductListRequest
                };

                var result = await _mediator.Send(request);

                Response.Headers.Add("X-Pagination", Newtonsoft.Json.JsonConvert.SerializeObject(result.MetaData));

                return Ok(_mapper.Map<IEnumerable<GetProductToShowViewModel>>(result.Values));

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
