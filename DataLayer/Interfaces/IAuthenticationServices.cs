using DataLayer.DTOs.Authentication;
using DataLayer.DTOs.Users;
using DataLayer.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Interfaces
{
    public interface IAuthenticationServices
    {
        Task<ServiceResponse<CredentialDTO>> LoginAsync(UserLoginDTO userdata);
        Task verifyEmailAsync(string userid);
        Task<ServiceResponse<TokenDTO>> refreshTokenAsync(string reftoken);
    }
}
