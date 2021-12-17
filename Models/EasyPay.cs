using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace CommercialAgriEnterprise.Models
{
    public class EasyPay
    {
        public EasyPay(string _ReferenceNo, decimal? _transactionamount, string _returnurl, string _IPAddress)
        {
            ReferenceNo = _ReferenceNo;
            transactionamount = _transactionamount;
            returnurl = _returnurl;
            IPAddress = _IPAddress;
        }

        private string IPAddress { get; set; }
        private decimal? transactionamount { get; set; }
        private string merchantid { get { return "153596"; } }
        private string submerchantid { get { return "222"; } }
        //private string mandatoryfields { get { return encryptFile(ReferenceNo + '|' + submerchantid + '|' + transactionamount, key); } }
        private string optionalfields { get { return encryptFile(encryptedreturnurl, key); } }
        private string returnurl { get; set; }
        private string encryptedreturnurl { get { return encryptFile(returnurl, key); } }
        private string paymode { get { return encryptFile("9", key); } }
        public static string key { get { return "1515002835901000"; } }
        private string ReferenceNo { get; set; }

        public string url
        {
            get
            {
                CommercialAgriEnterpriseEntities orm = new CommercialAgriEnterpriseEntities();
                int lastTranID = orm.Payments.Where(g => g.ReferenceNo == ReferenceNo).Count();
                dynamic TranID = null;
                dynamic tranStatus = null;
                if (lastTranID > 0)
                {
                    TranID = ReferenceNo.Substring(0, 3) + ReferenceNo.Substring(4, 3) + ReferenceNo.Substring(10, 2) + ReferenceNo.Substring(13, 2) + ReferenceNo.Substring(16, ReferenceNo.Length - 16) + (lastTranID + 1);
                }
                else
                {
                    TranID = ReferenceNo.Substring(0, 3) + ReferenceNo.Substring(4, 3) + ReferenceNo.Substring(10, 2) + ReferenceNo.Substring(13, 2) + ReferenceNo.Substring(16, ReferenceNo.Length - 16) + 1;
                }
                var k = orm.Payments.Where(z => z.ReferenceNo == ReferenceNo).OrderByDescending(z => z.TransactionID).Select(x => new { x.TransactionStatus, x.BankResponseCode }).FirstOrDefault();
                if (k == null)
                {
                    tranStatus = "Pending";
                }
                else if (k.TransactionStatus == "Pending")
                {
                    tranStatus = "Pending";
                }
                else if (k.TransactionStatus == "Success" && k.BankResponseCode == "E000")
                {
                    tranStatus = "Error";
                }
                else if (k.TransactionStatus == "Error")
                {
                    tranStatus = "Error";
                }
                else
                {
                    tranStatus = "Unknown";
                }
                orm.Payments.Add(new Payment { ReferenceNo = ReferenceNo, TransactionID = TranID, TransactionStatus = tranStatus, TransactionDate = DateTime.Now.Date, TransactionTime = DateTime.Now.TimeOfDay, SubMerchantID = submerchantid, MerchantID = merchantid, UserIPAddress = IPAddress, FinancialYear = GetCurrentFinancialYear() });
                try
                {
                    orm.SaveChanges();
                    return "https://eazypay.icicibank.com/EazyPG?" + "merchantid=" + merchantid + "&mandatory fields=" + encryptFile(TranID + '|' + submerchantid + '|' + transactionamount, key) + "&optional fields=" + null + "&returnurl=" + encryptedreturnurl + "&Reference No=" + encryptFile(TranID, key) + "&submerchantid=" + encryptFile(submerchantid, key) + "&transaction amount=" + encryptFile(transactionamount.ToString(), key) + "&paymode=" + paymode;
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                    return "An error occured. Please try again.";
                }
            }
        }

        public static string encryptFile(string textToEncrypt, string key)
        {
            RijndaelManaged rijndaelCipher = new RijndaelManaged();
            rijndaelCipher.Mode = CipherMode.ECB;
            rijndaelCipher.Padding = PaddingMode.PKCS7;
            rijndaelCipher.KeySize = 0x80; rijndaelCipher.BlockSize = 0x80;
            byte[] pwdBytes = Encoding.UTF8.GetBytes(key);
            byte[] keyBytes = new byte[0x10];
            int len = pwdBytes.Length;
            if (len > keyBytes.Length)
            {
                len = keyBytes.Length;
            }
            Array.Copy(pwdBytes, keyBytes, len);
            rijndaelCipher.Key = keyBytes;
            rijndaelCipher.IV = keyBytes;
            ICryptoTransform transform = rijndaelCipher.CreateEncryptor();
            byte[] plainText = Encoding.UTF8.GetBytes(textToEncrypt);
            return Convert.ToBase64String(transform.TransformFinalBlock(plainText, 0, plainText.Length));
        }

        private static string ByteArrayToHexString(byte[] ba)
        {
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
            {
                hex.AppendFormat("{0:x2}", b);
            }
            return hex.ToString();
        }

        public static string GenerateSHA512String(string inputString)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(inputString);
            SHA512Managed sha512hasstring = new SHA512Managed();
            byte[] hash = sha512hasstring.ComputeHash(bytes);
            return ByteArrayToHexString(hash);
        }

        public static string GetCurrentFinancialYear()
        {
            int CurrentYear = DateTime.Today.Year;
            int PreviousYear = DateTime.Today.Year - 1;
            int NextYear = DateTime.Today.Year + 1;
            string PreYear = PreviousYear.ToString();
            string NexYear = NextYear.ToString();
            string CurYear = CurrentYear.ToString();
            string FinYear = null;
            if (DateTime.Today.Month > 3)
                FinYear = CurYear + "-" + NexYear.Substring(2, 2);
            else
                FinYear = PreYear + "-" + CurYear.Substring(2, 2);
            return FinYear.Trim();
        }
    }
}