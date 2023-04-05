using HexaShop.Application.Constracts.PersistanceContracts;
using HexaShop.Application.Dtos.CategoryDtos.Queries;
using HexaShop.Application.Dtos.Common;
using HexaShop.Application.Features.CategoryFeatures.Requests.Queries;
using HexaShop.Domain;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HexaShop.EndPoint.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TestController : ControllerBase
    {


        private readonly IUnitOfWork _unitOfWork;
        private readonly IMediator _mediator;


        public TestController(IUnitOfWork unitOfWork, IMediator mediator)
        {
            _unitOfWork = unitOfWork;
            _mediator = mediator;
        }



        [HttpPost]
        public async Task<IActionResult> GetLatestPost([FromBody] GetListBaseDto getCategoryListRequestDto)
        {
            //var request = new GetParentCategoryListQR()
            //{
            //    GetCategoryList = getCategoryListRequestDto
            //};

            //var result = _mediator.Send(request);

            return Ok("jhgjhg");
        }

    }
}
