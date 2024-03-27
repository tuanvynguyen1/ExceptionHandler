using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Encryption
{
    public interface IPasswordHasher
    {
        string Hash(string password);
        
    }
}
