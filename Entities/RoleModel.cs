using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class RoleModel : BaseModel
    {
        public int Id { get; set; }
        public string RoleName { get; set; }
        public string RoleDescription { get; set; } = string.Empty; 

    }
}
