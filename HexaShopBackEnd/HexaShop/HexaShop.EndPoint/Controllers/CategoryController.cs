using AutoMapper;
using HexaShop.Application.Dtos.CategoryDtos.Commands;
using HexaShop.Application.Dtos.CategoryDtos.Queries;
using HexaShop.Application.Features.CategoryFeatures.Requests.Commands;
using HexaShop.Application.Features.CategoryFeatures.Requests.Queries;
using HexaShop.Common.CommonExtenstionMethods;
using HexaShop.EndPoint.Models.ViewModels.CategoryController;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HexaShop.EndPoint.Controllers
{
    [Route("api/[controller]/")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public CategoryController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        /// <summary>
        /// create category
        /// </summary>
        /// <param name="createCategoryDto"></param>
        /// <returns></returns>
        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] CreateCategoryDto createCategoryDto)
        {
            try
            {
                var createCategoryRequest = new CreateCategoryCR()
                {
                    CreateCategoryDto = createCategoryDto
                };

                var createCategoryResult =  await _mediator.Send(createCategoryRequest);

                if(!createCategoryResult.IsSuccess)
                {
                    createCategoryResult.ThrowException<int>();
                }

                return Ok(createCategoryResult.Message);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("Get/{id}")]
        public async Task<ActionResult<CategoryDto>> Get(int id)
        {
            try
            {
                var getCategoryRequest = new GetCategoryQR()
                {
                    Id = id
                };

                var category = await _mediator.Send(getCategoryRequest);


                return Ok(category);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// edit category
        /// </summary>
        /// <param name="editCategoryDto"></param>
        /// <returns></returns>
        [HttpPost("Edit")]
        public async Task<IActionResult> Edit([FromBody] EditCategoryDto editCategoryDto)
        {
            try
            {
                var request = new EditCategoryCR()
                {
                    EditCategoryDto = editCategoryDto
                };

                var editResult = await _mediator.Send(request);

                return Ok(editResult.Message);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// get parent categories.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("GetList")]
        public async Task<ActionResult<List<CategoryDto>>> GetList([FromBody] GetCategoryListRequest model)
        {
            try
            {

                var request = new GetParentCategoryListQR()
                {
                    GetCategoryList = model
                };
                var result = await _mediator.Send(request);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
