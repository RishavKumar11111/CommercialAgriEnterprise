using Microsoft.AspNet.Identity;
using System.Security.Cryptography;
using System.Text;

namespace CommercialAgriEnterprise.Models
{
    public class Crypt
    {
        public class ClearPassword : IPasswordHasher
        {
            public string HashPassword(string password)
            {
                return ConvertToSHA256(password);
            }

            public PasswordVerificationResult VerifyHashedPassword(string hashedPassword, string providedPassword)
            {
                if (hashedPassword.Equals(providedPassword))
                    return PasswordVerificationResult.Success;
                else return PasswordVerificationResult.Failed;
            }

            public string ConvertToSHA256(string pass)
            {
                byte[] bytes = Encoding.UTF8.GetBytes(pass);
                SHA256Managed sha256hasstring = new SHA256Managed();
                byte[] hash = sha256hasstring.ComputeHash(bytes);
                return ByteArrayToHexString(hash);
            }

            public static string ByteArrayToHexString(byte[] ba)
            {
                StringBuilder hex = new StringBuilder(ba.Length * 2);
                foreach (byte b in ba)
                {
                    hex.AppendFormat("{0:x2}", b);
                }
                return hex.ToString();
            }
        }
    }
}