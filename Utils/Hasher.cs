using Hackathon.Interfaces;
using System.Security.Cryptography;


namespace Hackathon.Utils
{
    public class Hasher : IHasher
    {
        private const int saltSize = 16;
        private const int keySize = 32;
        private const int iterCount = 100000;
        private static readonly HashAlgorithmName _hashAlgorithmName = HashAlgorithmName.SHA256;
        private const string delimiter = ";";

        public string GetHash(string password)
        {
            byte[] salt = RandomNumberGenerator.GetBytes(saltSize);
            byte[] hash = Rfc2898DeriveBytes.Pbkdf2(
                password, 
                salt, 
                iterCount, 
                _hashAlgorithmName, 
                keySize);

            return string.Join(delimiter, Convert.ToBase64String(salt), Convert.ToBase64String(hash));
        }

        public bool Verify(string passwordHash, string inputPassword) 
        {
            try
            {
                // It separates password hash by <delimiter>
                string[] soup = passwordHash.Split(delimiter);
                byte[] pSalt = Convert.FromBase64String(soup[0]);
                byte[] pHash = Convert.FromBase64String(soup[1]);

                byte[] ipHash = Rfc2898DeriveBytes.Pbkdf2(
                    inputPassword, 
                    pSalt, 
                    iterCount,
                    _hashAlgorithmName, 
                    keySize);

                Console.WriteLine(passwordHash, Convert.ToBase64String(ipHash));

                return CryptographicOperations.FixedTimeEquals(pHash, ipHash);
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
