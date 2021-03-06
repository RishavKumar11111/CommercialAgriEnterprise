//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CommercialAgriEnterprise.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Payment
    {
        public string ReferenceNo { get; set; }
        public string TransactionID { get; set; }
        public string TransactionStatus { get; set; }
        public System.DateTime TransactionDate { get; set; }
        public System.TimeSpan TransactionTime { get; set; }
        public string BankTransactionID { get; set; }
        public string BankResponseCode { get; set; }
        public Nullable<System.DateTime> BankResponseDate { get; set; }
        public Nullable<System.TimeSpan> BankResponseTime { get; set; }
        public Nullable<decimal> ServiceTax { get; set; }
        public Nullable<decimal> ProcessingFee { get; set; }
        public Nullable<decimal> TotalAmount { get; set; }
        public Nullable<decimal> TransactionAmount { get; set; }
        public string InterchangeValue { get; set; }
        public string TDR { get; set; }
        public string PaymentMode { get; set; }
        public string TPS { get; set; }
        public string SubMerchantID { get; set; }
        public string MerchantID { get; set; }
        public string UserIPAddress { get; set; }
        public string FinancialYear { get; set; }
        public string PullResponse { get; set; }
    
        public virtual EasyPayErrorCode EasyPayErrorCode { get; set; }
        public virtual BeneficiaryDetail BeneficiaryDetail { get; set; }
    }
}
