using System.Security.Cryptography;

namespace MLSZ.Shared
{
    public static class Shared
    {
        public static (byte[], byte[]) CreatePasswordHash(string password)
        {
            using (var hmac = new HMACSHA512())
            {
                return (hmac.Key, hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password)));
            }
        }
    }
}
