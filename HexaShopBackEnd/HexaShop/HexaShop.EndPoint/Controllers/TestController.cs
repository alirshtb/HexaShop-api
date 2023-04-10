using HexaShop.Application.Constracts.PersistanceContracts;
using HexaShop.Application.Dtos.CategoryDtos.Queries;
using HexaShop.Application.Dtos.Common;
using HexaShop.Application.Features.CategoryFeatures.Requests.Queries;
using HexaShop.Domain;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using System.Security.Claims;

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



        [HttpGet]
        public async Task<IActionResult> Test()
        {

            var claimIdentity = (User.Identity as ClaimsIdentity);

            var userName = claimIdentity.FindFirst("UserName");

            var email = User.Claims.FirstOrDefault(c => c.Type.Contains(JwtRegisteredClaimNames.Email));

            var claim = User.Claims?.FirstOrDefault(c => c.Type.Equals("name", StringComparison.OrdinalIgnoreCase))?.Value;


            return Ok("jhgjhg");
        }

    }
}
