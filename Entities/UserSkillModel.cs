
using Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;
using System.Text.Json.Serialization;

namespace Entities
{
    public class UserSkillModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        public int UserId { get; set; }
        [ForeignKey(nameof(UserId))]

        public int SkillId { get; set; }
        [ForeignKey(nameof(SkillId))]
        public virtual SkillModel Skill { get; init; }
        [JsonIgnore]
        public virtual UsersModel User { get; init; }
        public byte YearOfExperience { get; set; } = 0;
    }
}
