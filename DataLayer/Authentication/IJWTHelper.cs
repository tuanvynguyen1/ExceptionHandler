using DataLayer.DTOs.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Authentication
{
    public interface IJWTHelper
    {
        Task<string> GenerateJWTToken(int id, DateTime expire, UserDTO user);
        Task<string> GenerateJWTRefreshToken(int id, DateTime expire);
        Task<String> GenerateJWTMailAction(int id, DateTime expire, string action);
        ClaimsPrincipal ValidateToken(string jwtToken);
    }
}
