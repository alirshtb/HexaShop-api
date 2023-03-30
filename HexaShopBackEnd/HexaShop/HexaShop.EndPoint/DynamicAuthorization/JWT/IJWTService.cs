using HexaShop.ApiEndPoint.Models.Dtos.IdentityDtos;

namespace HexaShop.ApiEndPoint.DynamicAuthorization.JWT
{
    public interface IJWTService
    {
        Task<RequestTokenResultDto> CreateTokenAsync(RequestTokenDto request);
        Task<RequestTokenResultDto> CreateTokenAsync(string refreshToken);
    }
}
