using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.DTOs.Users
{
    public class JwtDTO
    {
        public string Token { get; set; }
        public DateTime ExpiredDate { get; set; }

        public UsersModel User { get; set; }
    }
}
