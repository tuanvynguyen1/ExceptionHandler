using Data;
using DataLayer.Authentication;
using DataLayer.DTOs.Users;
using DataLayer.Encryption;
using DataLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Services
{
    public class AuthenticationServices : IAuthenticationServices
    {
        private readonly AppDbContext _context;
        private readonly IPasswordHasher _pwHasher;
        private readonly IJWTHelper _jWTHelper;
        public AuthenticationServices(AppDbContext context, IPasswordHasher pwhasher, IJWTHelper jWTHelper)
        {
            _context = context;
            _pwHasher = pwhasher;
            _jWTHelper = jWTHelper;
        }
        public async Task<(string token, string refreshToken)> LoginAsync(UserLoginDTO userdata)
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(x => x.UserName == userdata.UserName);
                if (user != null)
                {
                    var checkCredential = _pwHasher.verify(userdata.Password, user.Password);
                    if (checkCredential)
                    {

                        return (_jWTHelper.GenerateJWT(user.Id, DateTime.UtcNow.AddMinutes(10)), _jWTHelper.GenerateJWT(user.Id, DateTime.UtcNow.AddMonths(6)));
                    }
                }

                return (null, null);
            }
            catch
            {
                throw;
            }
        }
    }
}
