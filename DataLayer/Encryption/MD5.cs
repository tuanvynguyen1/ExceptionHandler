using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Encryption
{
    public class MD5 : IPasswordHasher
    {
        public string Hash(string password)
        {
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(password);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                return Convert.ToHexString(hashBytes);

            }
        }

        

        public bool verify(string password, string encryptedPassword)
        {
            if (Hash(password) == encryptedPassword) return true;
            return false;
        }
    }
}
