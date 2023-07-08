using BLL.Abstractions.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace BLL
{
    public class SHA256Hasher : IHasher
    {
        public string Hash(string password)
        {
            using HashAlgorithm algorithm = SHA256.Create();

            byte[] encryptedByteArray = algorithm.ComputeHash(Encoding.ASCII.GetBytes(password));

            return Encoding.ASCII.GetString(encryptedByteArray);
        }
    }
}