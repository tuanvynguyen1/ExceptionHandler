namespace DataLayer.Encryption
{
    public class Bcrypt : IPasswordHasher
    {
        public string Hash(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        

        public bool verify(string password, string encryptedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, encryptedPassword); 

        }
    }
}
