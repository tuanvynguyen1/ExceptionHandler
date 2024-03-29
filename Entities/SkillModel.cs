using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class SkillModel : BaseModel
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SkillId { get; set; }
        [Required]
        [StringLength(50, MinimumLength =3)]
        public string SkillName { get; set; }
        [Required]
        [DataType(DataType.Text)]
        public string SkillDescription { get; set;}
    }
}
