using AutoMapper;
using DataLayer.DTOs.Users;
using DataLayer.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using Microsoft.EntityFrameworkCore;
using static DataLayer.Response.EServiceResponseTypes;
using DataLayer.Response;
using DataLayer.DTOs.Role;
using System.Data;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace DataLayer.Services
{
    public class UserServices : IUserServices
    {
        private readonly ILogger _logger;
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public UserServices(ILogger<UserServices> logger, AppDbContext context, IMapper mapper)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
        }

        public async Task<UserDTO?> GetUserByIdAsync(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
            return _mapper.Map<UserDTO>(user); 
        }

        public async Task<ServiceResponse<UserInfoDTO>> GetUserInfoAsync(string? id)
        {
            var serviceResponse = new ServiceResponse<UserInfoDTO>();
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == int.Parse(id));  
            if (user == null)
            {

                serviceResponse.ResponseType = EResponseType.NotFound;
            }
            else
            {
                UserInfoDTO u = _mapper.Map<UserInfoDTO>(user);
                serviceResponse.Data = u;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<UserDTO>> RegisterAsync(UserRegisterDTO user)
        {
            var serviceResponse = new ServiceResponse<UserDTO>();
            try
            {
                var toAdd = _mapper.Map<UsersModel>(user);
                var role = await _context.Roles.FirstAsync(x => x.RoleName == "TimViec");
                await _context.UserRoles.AddAsync(new UserRoleModel()
                {
                    Role = role,
                    User = toAdd
                });
                await _context.Users!.AddAsync(toAdd);
                await _context.SaveChangesAsync();
                serviceResponse.Data = _mapper.Map<UserDTO>(toAdd);
            }
            catch (DbUpdateException ex)
            {
                serviceResponse.ResponseType = EResponseType.CannotCreate;
                serviceResponse.Message = "Username/Email already taken by another User. Please reset or choose different.";
            }
            catch { throw; }
            return serviceResponse;
        }

        public async Task<ServiceResponse<UserDTO>> SelectRole(SelectRoleDTO Role, string? userid)
        {
            RoleModel selectedRole = await _context.Roles.Where(x=>x.RoleAccessLevel == 1).FirstOrDefaultAsync(x => x.RoleName == Role.RoleName);
            var serviceResponse = new ServiceResponse<UserDTO>();

            try
            {
                var userModel = await _context.Users.FirstOrDefaultAsync(x => x.Id == int.Parse(userid));
                if (await _context.UserRoles.Where(x => x.User == userModel  ).Where(x=>x.Role == selectedRole).FirstOrDefaultAsync() != null)
                {
                    serviceResponse.ResponseType = EResponseType.CannotUpdate;
                    serviceResponse.Message = "You have this role already.";
                }
                else
                {
                    await _context.UserRoles.AddAsync(new UserRoleModel()
                    {
                        Role = selectedRole,
                        User = userModel
                    });
                    await _context.SaveChangesAsync();
                    serviceResponse.Data = _mapper.Map<UserDTO>(userModel);
                }
            }
            catch (Exception ex)
            {
                serviceResponse.ResponseType = EResponseType.CannotUpdate;
                serviceResponse.Message = "Something wrong.";
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<UserDTO>> updateUserInfo(UserInfoDTO updateUser, string? id)
        {

            var serviceResponse = new ServiceResponse<UserDTO>();


            try {

                var user = await _context.Users.FirstOrDefaultAsync(c => c.Id == (id == null ? -1 : int.Parse(id)));
                if (user == null)
                {

                    serviceResponse.ResponseType = EResponseType.NotFound;
                }
                else
                {
                    user = _mapper.Map(updateUser, user);
                    serviceResponse.ResponseType = EResponseType.Success;
                    serviceResponse.Message = "Update User Successful";
                    serviceResponse.Data = _mapper.Map<UserDTO>(user);
                }
            }
            catch (DbUpdateException ex)
            {
                serviceResponse.ResponseType = EResponseType.CannotUpdate;
                serviceResponse.Message = "Error Occur While updating data.";
            }
            return serviceResponse;
        }

        
    }
}
