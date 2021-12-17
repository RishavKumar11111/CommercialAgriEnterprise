using System;
using System.IO;
using System.Net;
using System.Text;

namespace CommercialAgriEnterprise.Models
{
    public class SMSGateway
    {
        public static string SendSMS(string MobileNumber, string SMS)
        {
            string userId = "egovodisha";
            string authCode = "7823915";
            string RevertText = "-- Invalid mobile number --";
            try
            {
                if (MobileNumber.Length >= 10)
                {
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://mkisan.gov.in/ksewa/ksewa.aspx");
                    request.Timeout = 900000;
                    request.ReadWriteTimeout = 900000;
                    request.ProtocolVersion = HttpVersion.Version10; request.UserAgent = ".NET Framework Example Client";
                    request.UserAgent = "Mozilla/4.0 (compatible; MSIE 5.0; Windows98; DigExt)";
                    request.Method = "POST";
                    String query = "" + "txtMsg=" + Uri.EscapeDataString(SMS) + "&mobileNo=" + Uri.EscapeDataString(MobileNumber) + "&SMSMode =" + Uri.EscapeDataString("T") + "&userId=" + Uri.EscapeDataString(userId) + "&authCode=" + Uri.EscapeDataString(authCode);
                    byte[] byteArray = Encoding.ASCII.GetBytes(query);
                    request.ContentType = "application/x-www-form-urlencoded";
                    request.ContentLength = byteArray.Length;
                    Stream dataStream = request.GetRequestStream();
                    dataStream.Write(byteArray, 0, byteArray.Length);
                    dataStream.Close();
                    WebResponse response = request.GetResponse();
                    String Status = ((HttpWebResponse)response).StatusDescription;
                    dataStream = response.GetResponseStream();
                    StreamReader reader = new StreamReader(dataStream);
                    string responseFromServer = reader.ReadToEnd();
                    reader.Close();
                    dataStream.Close();
                    response.Close();
                    RevertText = responseFromServer;
                }
                else
                {
                    RevertText = "-- Invalid mobile number --";
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
                RevertText = "-- Remote site server is not responding. --";
            }
            return RevertText;
        }
    }
}