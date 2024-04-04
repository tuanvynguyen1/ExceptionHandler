using DataLayer.DTOs.Users;
using DataLayer.Email;
using DataLayer.Interfaces;
using Entities.Config;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Authentication
{
    public class JWTHelper : IJWTHelper
    {
        private readonly TokenSettings _tokenSetting;

        public JWTHelper(IOptions<TokenSettings> tokenSetting) { 
        
            _tokenSetting = tokenSetting.Value;
        }
        public async Task<string> GenerateJWTToken(int id, DateTime expire, UserDTO user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            
            var key = Encoding.ASCII.GetBytes(_tokenSetting.Secret);
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, id.ToString())
            };
            
            foreach ( var role in user.Roles )
            {
                claims.Add(new Claim(ClaimTypes.Role, role.RoleName));
            }
            var token = new JwtSecurityToken(
                claims: claims,
                expires: expire,
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            );
            return tokenHandler.WriteToken(token);
        }
        public async Task<string> GenerateJWTRefreshToken(int id, DateTime expire, UserDTO user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(_tokenSetting.Secret);
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, id.ToString())
            };
            var token = new JwtSecurityToken(
                claims: claims,
                expires: expire,
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            );
            return tokenHandler.WriteToken(token);
        }

        public ClaimsPrincipal ValidateToken(string jwtToken)
        {

            try
            {
                var validationParameters = new TokenValidationParameters
                {
                    ValidateLifetime = true,
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenSetting.Secret)),
                };

                var principal = new JwtSecurityTokenHandler().ValidateToken(jwtToken, validationParameters, out _);
                return principal;
            }
            catch (Exception ex)
            {
                return new ClaimsPrincipal();
            }
        }
    }
}
