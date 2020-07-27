using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using JwtDemo.AppServices;
using JwtDemo.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace JwtDemo._Impl
{
    public class AuthAppService : IAuthAppService
    {
        private readonly AuthSetting _appSettings;

        public AuthAppService(IOptionsSnapshot<AuthSetting> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public AuthClientResult AuthClient(AuthClientVo vo)
        {
            var authClientResult = new AuthClientResult();
            if (string.IsNullOrWhiteSpace(vo.ClientId))
            {
                authClientResult.Message = "Bad ClientId";
                return authClientResult;
            }

            if (_appSettings.ClientConnectKey != vo.ClientConnectKey)
            {
                authClientResult.Message = "Bad ClientConnectKey";
                return authClientResult;
            }
            
            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, vo.ClientId)
                }),
                Expires = DateTime.UtcNow.AddYears(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var theToken = tokenHandler.WriteToken(securityToken);

            authClientResult.Message = "OK";
            authClientResult.Success = true;
            authClientResult.Data = theToken;
            return authClientResult;
        }
    }
}