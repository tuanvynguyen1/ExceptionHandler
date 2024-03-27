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
        public async Task<ServiceResponse<UserDTO>> RegisterAsync(UserRegisterDTO user)
        {
            var serviceResponse = new ServiceResponse<UserDTO>();
            try
            {
                var toAdd = _mapper.Map<UsersModel>(user);
                await _context.Users!.AddAsync(toAdd);
                await _context.SaveChangesAsync();
                serviceResponse.Data = _mapper.Map<UserDTO>(toAdd);
            }
            catch (DbUpdateConcurrencyException)
            {
                serviceResponse.ResponseType = EResponseType.CannotCreate;
                serviceResponse.Message = "Email already in use by another User. Please login or choose different.";
            }
            catch { throw; }
            return serviceResponse;
        }
    }
}
