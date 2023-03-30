using HexaShop.ApiEndPoint.DynamicAuthorization.JWT;
using HexaShop.ApiEndPoint.Models.Dtos.IdentityDtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace HexaShop.EndPoint.Controllers
{
    [Route("api/[controller]/")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly IOptions<JwtOptions> _jwtOptions;
        private readonly IJWTService _jwtService;

        public AuthController(IOptions<JwtOptions> jwtOptions, IJWTService jwtService)
        {
            _jwtOptions = jwtOptions;
            _jwtService = jwtService;
        }

        [HttpPost("GetToken")]
        public async Task<ActionResult<RequestTokenResultDto>> GetToken([FromBody] RequestTokenDto requestTokenDto)
        {


            var token = await _jwtService.CreateTokenAsync(requestTokenDto);
            return new RequestTokenResultDto()
            {
                UserName = requestTokenDto.UserName,
                UserToken = token.UserToken,
                RefreshToken = token.RefreshToken,
                RefreshTokenExpiration = token.RefreshTokenExpiration,
                TokenExpiration = token.TokenExpiration
            };

        }

        [HttpPost("GetRefreshTokne")]
        public async Task<ActionResult<RequestTokenResultDto>> GetRefreshTokne([FromHeader] string refreshToken)
        {
            var token = await _jwtService.CreateTokenAsync(refreshToken);
            return token;
        }

    }
}
