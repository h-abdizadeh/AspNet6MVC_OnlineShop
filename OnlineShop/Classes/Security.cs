using System.Security.Cryptography;
using System.Text;

namespace OnlineShop.Classes
{
    public class Security
    {

        public static string GetHash(string str)
        {
            byte[] inputBytes;
            byte[] outputBytes;

            inputBytes = ASCIIEncoding.Default.GetBytes(str);

            MD5 md5 = new MD5CryptoServiceProvider();
            outputBytes = md5.ComputeHash(inputBytes);

            return BitConverter.ToString(outputBytes);
        }

    }
}
