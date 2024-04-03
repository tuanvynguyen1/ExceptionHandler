using DataLayer.DTOs.Users;
using DataLayer.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Interfaces
{
    public interface IUserServices
    {
        Task<ServiceResponse<UserDTO>> RegisterAsync(UserRegisterDTO user);
        Task<ServiceResponse<UserDTO>> updateUserInfo(UserInfoDTO updateUser, string? id);
        Task<ServiceResponse<UserInfoDTO>> GetUserInfoAsync(string? id);
        Task<UserDTO?> GetUserByIdAsync(int id);
    }
}
