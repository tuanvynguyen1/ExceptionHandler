using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Encryption
{
    public interface IEncryption <T>
    {
        public string Encrypt(T data);
        public T Decrypt(string data);
    }
}
