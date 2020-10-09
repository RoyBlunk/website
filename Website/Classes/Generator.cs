using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace Website.Classes
{
    public abstract class Generator
    {
        public const int SaltSize = 64;
        public const int HashSize = 128;
        public const int Iterations = 10000;

        public static byte[] CreateHash(string input, byte[] salt)
        {
            if (!String.IsNullOrEmpty(input) && salt != null && salt.Length > 0)
            {
                Rfc2898DeriveBytes pdbdf2 = new Rfc2898DeriveBytes(input, salt, Iterations);
                return pdbdf2.GetBytes(HashSize);
            }
            return null;
        }

        public static byte[] CreateSalt()
        {
            RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();
            byte[] salt = new byte[SaltSize];
            provider.GetBytes(salt);

            return salt;
        }
    }
}