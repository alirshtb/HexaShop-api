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
    [Route("api/[controller]/[action]/")]
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
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCategoryDto createCategoryDto)
        {
            try
            {
                var createCategoryRequest = new CreateCategoryCR()
                {
                    CreateCategoryDto = createCategoryDto
                };

                var createCategoryResult = await _mediator.Send(createCategoryRequest);

                if (!createCategoryResult.IsSuccess)
                {
                    createCategoryResult.ThrowException<int>();
                }

                return CreatedAtAction("Get", controllerName: "Category", routeValues: new { id = createCategoryResult.ResultData }, value: null);

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
        [HttpGet("{id}")]
        [ActionName(nameof(Get))]
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
        [HttpPost]
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
        [HttpPost]
        public async Task<IActionResult> GetParentsList([FromBody] GetCategoryListRequestDto getCategoryListRequest)
        {
            try
            {

                var request = new GetParentCategoryListQR()
                {
                    GetCategoryList = getCategoryListRequest
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



    }
}
