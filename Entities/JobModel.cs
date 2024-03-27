using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.IdentityModel.Tokens.Configuration;
using Microsoft.Extensions.Hosting;

namespace Entities
{
    public class JobModel
    {
        public enum job_status
        {
            Closed,
            Full,
            Opened
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int JobId { get; set; }

        [Required]
        [StringLength(50)]
        public string JobTitle { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [Column(TypeName = "text")]
        public string JobDescription { get; set; }

        [Required]
        public job_status JobStatus { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime ExpiredDate { get; set; }
        public ICollection<JobSkillModel> Skill { get; }
    }
}
