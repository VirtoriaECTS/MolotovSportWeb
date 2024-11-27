namespace MolotovSportWeb.Components.Classes.Hash
{
    public class DataHash
    {
        public string Hash;
        public string Salt;

        public DataHash(string hash, string salt)
        {
            Hash = hash;
            Salt = salt;
        }
    }
}
