using DataLayer.DTOs.Authentication;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.DTOs.Users
{
    public class UserLoginDTO : PasswordDTO
    {
        [Required]
        public string UserName { get; set; }   
    }
}
