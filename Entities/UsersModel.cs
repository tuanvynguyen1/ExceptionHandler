using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Entities
{
    public class UsersModel: BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }


        [Required]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "First Name must contain at least 5 character and maximum to 50 character")]
        public string FirstName { get; set; }


        [Required]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Last Name must contain at least 5 character and maximum to 50 character")]
        public string LastName { get; set; }

        [Required]
        [RegularExpression("^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$", ErrorMessage = "Email format is not Valid!")]
        [StringLength(70, MinimumLength = 5, ErrorMessage = "Email length must between 5 and 70 character")]
        public string Email { get; set; }


        [JsonIgnore]
        [Required]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Password must contain atleast 8 character")]
        public string Password { get; set; }

        [JsonIgnore]
        [Required]
        public bool IsEmailConfirmed { get; set; } = false;


        [Required]
        [StringLength(50, MinimumLength = 8, ErrorMessage = "Username must contain atleast 8 character and maximum to 50 character")]

        public string UserName { get; set; }


        [Required]
        [RegularExpression("(84|0[3|5|7|8|9])+([0-9]{8})", ErrorMessage = "Phone number is not valid format!")]
        [StringLength(11, MinimumLength = 9)]
        public string PhoneNumber { get; set; }


        [Column(TypeName = "text")]
        public string? Avatar { get; set; }


        [JsonIgnore, Required]
        public bool isDeleted { get; set; } = false;

        public ICollection<UserVerifyModel> UserVerify { get; }


    }
}
