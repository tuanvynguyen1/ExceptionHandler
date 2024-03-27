using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Authentication
{
    public interface IJWTHelper
    {
        string GenerateJWT(int id, DateTime expire);
    }
}
