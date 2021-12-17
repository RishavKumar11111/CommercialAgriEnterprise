using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace CommercialAgriEnterprise.Models
{
    public class BussinessAril
    {
        public string validateuser(string UserId)
        {
            CommercialAgriEnterpriseEntities arg = new CommercialAgriEnterpriseEntities();
            var user = (from a in arg.MobileRegs where a.UserName == UserId && a.Status == false select a.Api_key).FirstOrDefault();
            if (user != null)
            {
                return user;
            }
            else
            {
                return string.Empty;
            }
        }

        public string ApikeyGenerate(string mob)
        {
            string Rkey = RandomString(5);
            string Apikey = mdfive(mob.Substring(1, 4) + Rkey);
            return Apikey;
        }

        private string RandomString(int length)
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public string mdfive(string pw)
        {
            string st = "";
            MD5CryptoServiceProvider md5obj = new MD5CryptoServiceProvider();
            byte[] bytesToHash = Encoding.ASCII.GetBytes(pw);
            bytesToHash = md5obj.ComputeHash(bytesToHash);
            foreach (byte b in bytesToHash)
            {
                st += b.ToString("x2");
            }
            return st;
        }

        public string Decrypt(string input, string key)
        {
            byte[] inputArray = Convert.FromBase64String(input);
            // byte[] inputArray = Convert.FromBase64String(ASCIIEncoding.UTF8.GetString(Convert.FromBase64String(input)).Split(',')[0]);
            TripleDESCryptoServiceProvider tripleDES = new TripleDESCryptoServiceProvider();
            MD5CryptoServiceProvider MD5 = new MD5CryptoServiceProvider();
            tripleDES.Key = MD5.ComputeHash(Encoding.ASCII.GetBytes(key));
            // tripleDES.Key = UTF8Encoding.UTF8.GetBytes(key);
            tripleDES.Mode = CipherMode.ECB;
            tripleDES.Padding = PaddingMode.PKCS7;
            ICryptoTransform cTransform = tripleDES.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(inputArray, 0, inputArray.Length);
            tripleDES.Clear();
            return Encoding.UTF8.GetString(resultArray);
        }

        public bool checkcredential(string userid, string pwd)
        {
            try
            {
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}