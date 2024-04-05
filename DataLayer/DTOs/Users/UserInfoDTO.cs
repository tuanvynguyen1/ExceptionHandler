using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.DTOs.Users
{
    public class UserInfoDTO
    {
        public string FirstName { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Last Name must contain at least 5 character and maximum to 50 character")]
        public string LastName { get; set; }
        [Required]
        [RegularExpression("(84|0)[3|5|7|8|9]([0-9]{8})", ErrorMessage = "Phone number is not valid format!")]
        [StringLength(11, MinimumLength = 9)]
        public string PhoneNumber { get; set; }
    }
}
