using AutoMapper;
using Data;
using DataLayer.Authentication;
using DataLayer.DTOs.Authentication;
using DataLayer.DTOs.Users;
using DataLayer.Encryption;
using DataLayer.Interfaces;
using DataLayer.Response;
using Hangfire;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DataLayer.Response.EServiceResponseTypes;

namespace DataLayer.Services
{
    public class AuthenticationServices : IAuthenticationServices
    {
        private readonly AppDbContext _context;
        private readonly IPasswordHasher _pwHasher;
        private readonly IJWTHelper _jWTHelper;
        private readonly IMapper _mapper;
        private readonly IEmailSender _mailSender;
        public AuthenticationServices(AppDbContext context, IPasswordHasher pwhasher, IJWTHelper jWTHelper, IMapper mapper, IEmailSender mailSender)
        {
            _context = context;
            _pwHasher = pwhasher;
            _jWTHelper = jWTHelper;
            _mapper = mapper;
            _mailSender = mailSender;
        }
        public async Task<ServiceResponse<CredentialDTO>> LoginAsync(UserLoginDTO userdata)
        {
            var serviceResponse = new ServiceResponse<CredentialDTO>();
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(x => x.UserName == userdata.UserName);
                if (user != null)
                {
                    var checkCredential = _pwHasher.verify(userdata.Password, user.Password);
                    if (checkCredential)
                    {
                        string token = _jWTHelper.GenerateJWT(user.Id, DateTime.UtcNow.AddMinutes(10));
                        string refreshToken = _jWTHelper.GenerateJWT(user.Id, DateTime.UtcNow.AddMonths(6));
                        serviceResponse.Data = _mapper.Map<CredentialDTO>(user);
                        serviceResponse.Data.RefreshToken = refreshToken;
                        serviceResponse.Data.Token = token;
                    }
                    else
                    {
                        serviceResponse.ResponseType = EResponseType.Unauthorized;
                        serviceResponse.Message = "Login Fail! Wrong password.";

                    }
                }
                else
                {
                    serviceResponse.ResponseType = EResponseType.Unauthorized;
                    serviceResponse.Message = "Login Fail! Could not found Account by Username!.";
                }

                return serviceResponse;
            }
            catch
            {

                throw;
            }
        }
        public async Task verifyEmailAsync()
        {
            //BackgroundJob.Enqueue(() => _mailSender.SendEmailAsync());
        }
    }
}
