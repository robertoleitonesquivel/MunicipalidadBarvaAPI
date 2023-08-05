using MuniBarva.COMMON.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace MuniBarva.COMMON
{
    public class Encrypt : IEncrypt
    {
        public async Task<string> Sha256(string str)
        {
            return await Task.Run(() =>
            {
                using (SHA256 sha256 = SHA256.Create())
                {
                    byte[] inputBytes = Encoding.UTF8.GetBytes(str);
                    byte[] hashBytes = sha256.ComputeHash(inputBytes);
                    return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
                }
            });

        }


    }
}
