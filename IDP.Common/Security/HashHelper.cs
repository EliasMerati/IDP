
using System.Security.Cryptography;
using System.Text;


namespace IDP.Common.Security
{
    public class HashHelper
    {
        private readonly RandomNumberGenerator random = RandomNumberGenerator.Create();
        public static string CreateHash(string value)
        {
            var algurithm = new SHA384CryptoServiceProvider();
            var bytevalue = Encoding.UTF8.GetBytes(value);
            var bytehash = algurithm.ComputeHash(bytevalue);
            return Convert.ToBase64String(bytehash);
        }
    }
}
