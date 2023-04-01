using HexaShop.ApiEndPoint.Models.Dtos.IdentityDtos;
using HexaShop.Common;
using HexaShop.Domain;
using HexaShop.Persistance;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HexaShop.ApiEndPoint.DynamicAuthorization.JWT
{
    public class JWTService : IJWTService
    {

        private readonly UserManager<AppIdentityUser> _userManager;
        private readonly IOptions<JwtOptions> _jwtOptions;
        private readonly HexaShopDbContext _db;

        public JWTService(UserManager<AppIdentityUser> userManager, IOptions<JwtOptions> jwtOptions, HexaShopDbContext db)
        {
            _userManager = userManager;
            _jwtOptions = jwtOptions;
            _db = db;
        }

        public async Task<RequestTokenResultDto> CreateTokenAsync(RequestTokenDto request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user == null)
            {
                throw new Exception(ApplicationMessages.UserNotFound);
            }
            if (!await _userManager.CheckPasswordAsync(user, request.Password))
            {
                throw new Exception(message: ApplicationMessages.InValidInformation);
            }

            if (!user.IsActive)
            {
                throw new Exception(ApplicationMessages.UserIsNotActive);
            }


            var nowTime = DateTime.UtcNow;

            var refreshToken = new RefreshToken()
            {
                Created = nowTime,
                Token = Guid.NewGuid().ToString(),
                Expires = nowTime.AddMinutes(_jwtOptions.Value.RefreshTokenExpirationMinutes)
            };

            if (user.RefreshTokens != null)
            {
                user.RefreshTokens.Add(refreshToken);
            }
            else
            {
                user.RefreshTokens = new List<RefreshToken>() { refreshToken };
            }
            await _db.SaveChangesAsync();



            return new RequestTokenResultDto()
            {
                UserName = user.UserName,
                UserToken = await CrreateTokenAsync(user, nowTime),
                RefreshToken = refreshToken.Token,
                TokenExpiration = nowTime.AddMinutes(_jwtOptions.Value.TokenExpirationMinutes),
                RefreshTokenExpiration = nowTime.AddMinutes(_jwtOptions.Value.RefreshTokenExpirationMinutes)
            };
        }

        public async Task<RequestTokenResultDto> CreateTokenAsync(string refreshToken)
        {
            var user = _db.Users.Include(u => u.RefreshTokens).FirstOrDefault(u => u.RefreshTokens.Any(rt => rt.Token == refreshToken));
            if (user == null)
            {
                throw new Exception(ApplicationMessages.InValidInformation);
            }

            if (!user.IsActive)
            {
                throw new Exception(ApplicationMessages.UserIsNotActive);
            }

            var userRefreshToken = user.RefreshTokens.SingleOrDefault(rt => rt.Token == refreshToken);
            if (userRefreshToken.IsActive == false)
            {
                throw new Exception("Invalid Refresh Token.");
            }

            var nowTime = DateTime.UtcNow;

            userRefreshToken.Revoked = nowTime;

            var newRefreshToken = new RefreshToken()
            {
                Created = nowTime,
                Token = Guid.NewGuid().ToString(),
                Expires = nowTime.AddMinutes(_jwtOptions.Value.RefreshTokenExpirationMinutes)
            };

            user.RefreshTokens.Add(newRefreshToken);

            await _db.SaveChangesAsync();

            return new RequestTokenResultDto()
            {
                UserName = user.UserName,
                UserToken = await CrreateTokenAsync(user, nowTime),
                RefreshToken = newRefreshToken.Token,
                TokenExpiration = nowTime.AddMinutes(_jwtOptions.Value.TokenExpirationMinutes),
                RefreshTokenExpiration = nowTime.AddMinutes(_jwtOptions.Value.RefreshTokenExpirationMinutes)
            };
        }


        private async Task<string> CrreateTokenAsync(AppIdentityUser user, DateTime time)
        {
            var roleIds = _db.UserRoles.Where(ur => ur.UserId == user.Id).Select(ur => ur.RoleId).ToList();
            var roleClaims = _db.RoleClaims.Where(rc => roleIds.Contains(rc.RoleId)).Select(rc => rc.ToClaim()).ToList();

            var claims = new[]
            {
                //new Claim(JwtRegisteredClaimNames.Iss, _jwtOptions.Issuer),
                //new Claim(JwtRegisteredClaimNames.Aud, _jwtOptions.Audience),
                new Claim("UserName", user.Email),
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.GivenName, user.UserName),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                //new Claim(JwtRegisteredClaimNames.Gender, user.HexaUser.Gender.ToString()),
                new Claim("FullName", user.FirstName + " " + user.LastName),
            }.Union(roleClaims);

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Value.Key));
            var signingCredential = new SigningCredentials(securityKey, algorithm: SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwtOptions.Value.Issuer,
                audience: _jwtOptions.Value.Audience,
                claims: claims,
                expires: time.AddMinutes(_jwtOptions.Value.TokenExpirationMinutes),
                signingCredentials: signingCredential
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}