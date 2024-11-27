

using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

namespace MolotovSportWeb.Components.Classes.Hash
{
    public class HashPassword
    {


        public DataHash hashing(string password)
        {

            byte[] salt = RandomNumberGenerator.GetBytes(128 / 8);

            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password!,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8));

            DataHash dataHash = new DataHash(hashed, Convert.ToBase64String(salt));
            return dataHash;
        }

        public bool unHashing(string password, string saltString, string hashDataBase)
        {
            byte[] salt = Convert.FromBase64String(saltString);

            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8));


            return hashed == hashDataBase;

        }
    }
}
