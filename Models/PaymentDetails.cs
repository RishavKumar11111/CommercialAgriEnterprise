using System;

namespace CommercialAgriEnterprise.Models
{
    public class PaymentDetails
    {
        public string ReferenceNo { get; set; }
        public string TransactionID { get; set; }
        public string TransactionStatus { get; set; }
        public DateTime TransactionDate { get; set; }
        public string TranDate { get { return TransactionDate.ToString("dd-MM-yyyy").Substring(0, 10); } }
        public TimeSpan TransactionTime { get; set; }
        public string TranTime { get { return TransactionTime.ToString().Substring(0, 8); } }
        public string BankResponseMessage { get; set; }
    }
}