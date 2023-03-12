using System.Security.Cryptography;
using System.Text;

namespace BLL
{
    public static class StringHasher
    {
        public static string HashStringSHA256(string input)
        {
            using HashAlgorithm algorithm = SHA256.Create();

            byte[] encryptedByteArray = algorithm.ComputeHash(Encoding.UTF8.GetBytes(input));

            return Encoding.UTF8.GetString(encryptedByteArray);
        }
    }
}