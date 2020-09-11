using Api.BLL.Entity;
using System;
using System.Security.Cryptography;

namespace Api.Services.Services
{
    // Password hashing: PBKDF2
    public class PasswordHashService
    {
        public const int SaltByteSize = 16;
        public const int HashByteSize = 20;
        public const int Pbkdf2Iterations = 1000;

        public static User HashPassword(string password)
        {
            var cryptoProvider = new RNGCryptoServiceProvider();
            byte[] salt = new byte[SaltByteSize];
            cryptoProvider.GetBytes(salt);

            var hash = GetPbkdf2Bytes(password, salt, Pbkdf2Iterations, HashByteSize);
            User user = new User();
            user.PasswordHash = Convert.ToBase64String(hash);
            user.Salt = Convert.ToBase64String(salt);
            return user;
        }

        public static bool ValidatePassword(string password, User correctUser)
        {
            var salt = Convert.FromBase64String(correctUser.Salt);
            var hash = Convert.FromBase64String(correctUser.PasswordHash);

            var testHash = GetPbkdf2Bytes(password, salt, Pbkdf2Iterations, hash.Length);
            return SlowEquals(hash, testHash);
        }

        private static bool SlowEquals(byte[] a, byte[] b)
        {
            var diff = (uint)a.Length ^ (uint)b.Length;
            for (int i = 0; i < a.Length && i < b.Length; i++)
            {
                diff |= (uint)(a[i] ^ b[i]);
            }
            return diff == 0;
        }

        private static byte[] GetPbkdf2Bytes(string password, byte[] salt, int iterations, int outputBytes)
        {
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt);
            pbkdf2.IterationCount = iterations;
            return pbkdf2.GetBytes(outputBytes);
        }
    }
}
