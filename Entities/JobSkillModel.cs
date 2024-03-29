using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    public class JobSkillModel : BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        public int JobId { get; set; }
        [ForeignKey(nameof(JobId))]
        public JobModel Job { get; init; }

        public int SkillId { get; set; }
        [ForeignKey(nameof(SkillId))]
        public SkillModel Skill { get; init; } 

        public byte YearOfExperience { get; set; } = 0;
    }
}
