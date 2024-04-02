using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace DataLayer.Encryption
{
    public class HMAC<T> : IEncryption<T>
    {
        private readonly IConfiguration _configuration;
        public HMAC(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        T IEncryption<T>.Decrypt(string data)
        {
            throw new NotImplementedException();
        }

        string IEncryption<T>.Encrypt(T data)
        {
            throw new NotImplementedException();
        }
    }
}
