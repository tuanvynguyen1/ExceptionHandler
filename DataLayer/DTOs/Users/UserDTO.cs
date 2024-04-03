using Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.DTOs.Users
{
    public class UserDTO
    {
        [Required]
        public string Email { get; set; }
            public string UserName { get; set; }
            [Required]
            public string FirstName { get; set; }
            [Required]
            public string LastName { get; set; }
            public string PhoneNumber { get; set; }
            public string? Avatar { get; set; }
            public List<RoleModel> Roles { get; set; }
    }
}
