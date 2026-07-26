using PrimeiroProjeto.Auth.Contract;
using System.Security.Cryptography;
using System.Text;

namespace PrimeiroProjeto.Auth.Tools
{
    public class Sha256PasswordHasher : IPasswordHasher
    {
        public string Hash(string password)
        {
            var inputBytes=Encoding.UTF8
                .GetBytes(password);
            var hashedBytes = SHA256.HashData
                (inputBytes);
            var builder = new StringBuilder();
            foreach(var b in hashedBytes)
            {
                builder.Append(b
                    .ToString("x2"));
            }
            return builder.ToString();
        }

        public bool Verify(
            string hashedPassword,
            string password)
        {
            return password == Hash(hashedPassword);
        }
    }
}
