using DataLayer.DTOs.Role;
using DataLayer.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Interfaces
{
    public interface IRoleServices
    {
        Task<ServiceResponse<IList<RolesDTO>>> getAllJobByLevel(int level);
    }
}
