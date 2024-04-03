using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Entities
{
    public class RoleModel : BaseModel
    {
        public int Id { get; set; }
        public string RoleName { get; set; }
        public string RoleDescription { get; set; } = string.Empty;
        [JsonIgnore]
        public virtual ICollection<UserRoleModel> UserRoles { get; set; }

    }
}
