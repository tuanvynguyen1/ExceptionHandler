using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class JWTModel:BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string TokenHashValue { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime ExpiredDate { get; set; }


        public UsersModel User { get; set; }
    }
}
