using AutoMapper;
using Data;
using DataLayer.Authentication;
using DataLayer.DTOs.Authentication;
using DataLayer.DTOs.Users;
using DataLayer.Encryption;
using DataLayer.Interfaces;
using DataLayer.Response;
using Entities;
using Hangfire;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
        private readonly IJwtServices _jwtServices;
        public AuthenticationServices(AppDbContext context, IPasswordHasher pwhasher, IJWTHelper jWTHelper, IMapper mapper, IEmailSender mailSender, IJwtServices jwtServices)
        {
            _context = context;
            _pwHasher = pwhasher;
            _jWTHelper = jWTHelper;
            _mapper = mapper;
            _mailSender = mailSender;
            _jwtServices = jwtServices;
        }
        public async Task<ServiceResponse<CredentialDTO>> LoginAsync(UserLoginDTO userdata)
        {
            var serviceResponse = new ServiceResponse<CredentialDTO>();
            try
            {
                var user = await _context.Users.Where(x => x.UserName == userdata.UserName).Include(x => x.UserRoles).ThenInclude(x => x.Role).FirstOrDefaultAsync();

                if (user != null)
                {
                    var checkCredential = _pwHasher.verify(userdata.Password, user.Password);
                    if (checkCredential)
                    {
                        var userDTO = _mapper.Map<UsersModel,UserDTO>(user);
                        string? token = await _jWTHelper.GenerateJWTToken(user.Id, DateTime.UtcNow.AddMinutes(10), userDTO);
                        string? refreshToken = await _jWTHelper.GenerateJWTRefreshToken(user.Id, DateTime.UtcNow.AddMonths(6), userDTO);


                        await _jwtServices.InsertJWTToken(new JwtDTO()
                        {
                            User = user,
                            ExpiredDate = DateTime.UtcNow.AddMonths(6),
                            Token = refreshToken,
                        });

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
        public async Task verifyEmailAsync(string userid)
        {
            //BackgroundJob.Enqueue(() => _mailSender.SendEmailAsync());
            var u = await _context.Users.FirstOrDefaultAsync(x => x.Id == int.Parse(userid));
            if (u == null || u.IsEmailConfirmed == true)
            {
                return;
            }
            BackgroundJob.Enqueue(() => _mailSender.SendEmailAsync(u.Email,"Confirm Your Email", "Confirm"));
        }
        public async Task<ServiceResponse<TokenDTO>> refreshTokenAsync(string reftoken)
        {
            var serviceResponse = new ServiceResponse<TokenDTO>();

            var claim = _jWTHelper.ValidateToken(reftoken);

            if (claim.HasClaim(claim => claim.Type == ClaimTypes.NameIdentifier))
            {
                var userid = claim.FindFirst(ClaimTypes.NameIdentifier)!.Value;

                var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == int.Parse(userid));
                if (user == null)
                {
                    serviceResponse.ResponseType = EResponseType.Unauthorized;
                    serviceResponse.Message = "Could not found User from token.";
                }
                else
                {

                    var userDTO = _mapper.Map<UsersModel, UserDTO>(user);

                    string? token = await _jWTHelper.GenerateJWTToken(user.Id, DateTime.UtcNow.AddMinutes(10), userDTO);

                    if (token == null)
                    {
                        serviceResponse.ResponseType = EResponseType.Unauthorized;
                        serviceResponse.Message = "Could not found User from token.";
                    }
                    else
                    {
                        TokenDTO _tokendto = new TokenDTO();
                        _tokendto.Token = token;
                        serviceResponse.Data = _tokendto;
                    }
                    
                }
            }
            else
            {
                serviceResponse.ResponseType = EResponseType.Unauthorized;
                serviceResponse.Message = "Could not found User from token.";
            }
            return serviceResponse;

        }
    }
}
