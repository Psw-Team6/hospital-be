using System;
using System.Security.Cryptography;

namespace HospitalLibrary.ApplicationUsers.Model
{
    public class PasswordHasher
    {
        private static readonly RNGCryptoServiceProvider Rng = new();
        private const int SaltSize = 16;
        private const int HashSize = 20;
        private const int Iterations = 10000;

        public static string HashPassword(string password)
        {
            byte[] salt;
            Rng.GetBytes(salt = new byte[SaltSize]);
            var key = new Rfc2898DeriveBytes(password, salt, Iterations);
            var hash = key.GetBytes(HashSize);
            var hashBytes = new byte[SaltSize + HashSize];
            Array.Copy(salt,0,hashBytes,0,SaltSize);
            Array.Copy(hash,0,hashBytes,SaltSize,HashSize);
            var base64Hash = Convert.ToBase64String(hashBytes); 
            return base64Hash;
        }

        public static bool VerifyPassword(string password, string base64Password)
        {
            var hashBytes = Convert.FromBase64String(base64Password);
            var salt = new byte[SaltSize];
            Array.Copy(hashBytes, 0 , salt,0,SaltSize);
            var key = new Rfc2898DeriveBytes(password, salt, Iterations);
            var hash = key.GetBytes(HashSize);
            for (var i = 0; i < HashSize; i++)
            {
                if (hashBytes[i + SaltSize] != hash[i])
                {
                    return false;
                }
            }
            return true;
        }
        
    }
}