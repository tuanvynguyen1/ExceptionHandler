using DataLayer.DTOs.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.DTOs.Authentication
{
    public class CredentialDTO : UserDTO
    {
        public string Token {  get; set; }
        public string RefreshToken { get; set; }
    }
}
