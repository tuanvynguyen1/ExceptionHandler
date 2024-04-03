﻿using DataLayer.DTOs.Users;
using DataLayer.Interfaces;
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
        private readonly IConfiguration _configuration;

        public JWTHelper(IConfiguration configuration) { 
        
            _configuration = configuration;
        }
        public async Task<string> GenerateJWT(int id, DateTime expire, UserDTO user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            
            var key = Encoding.ASCII.GetBytes(_configuration.GetSection("AppSettings:Secret").Value ?? throw new InvalidOperationException("Connection string 'Secret' not found."));
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
    }
}
