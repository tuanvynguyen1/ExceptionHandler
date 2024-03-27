using DataLayer.DTOs.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Interfaces
{
    public interface IAuthenticationServices
    {
        Task<(string token, string refreshToken)> LoginAsync(UserLoginDTO userdata);
    }
}
