using System;
using System.Linq;

namespace CommercialAgriEnterprise.Models
{
    public class TransactionReceipt
    {
        CommercialAgriEnterpriseEntities orm = new CommercialAgriEnterpriseEntities();
        FARMERDBEntities orm1 = new FARMERDBEntities();

        public string ReferenceNo { get; set; }
        public string FarmerID { get; set; }
        public string FarmerName { get { if ((orm.BeneficiaryDetails.Where(z => z.ReferenceNo == ReferenceNo).Select(x => x.RelationApplicantName).FirstOrDefault()) != null) { return orm.BeneficiaryDetails.Where(z => z.ReferenceNo == ReferenceNo).Select(x => x.RelationApplicantName).FirstOrDefault(); } else { return new FARMERDBEntities().M_FARMER_REGISTRATION.Where(z => z.NICFARMERID == FarmerID).Select(x => x.VCHFARMERNAME).FirstOrDefault(); } } }
        public string TransactionStatus { get; set; }
        public string BankTransactionID { get; set; }
        public string BankResponseCode { get; set; }
        public string BankResponseName { get; set; }
        public DateTime? BankTransactionDate { get; set; }
        public string BankTranDate { get { return Convert.ToDateTime(BankTransactionDate).ToString("dd/MM/yyyy").Substring(0, 10); } }
        public TimeSpan? BankTransactionTime { get; set; }
        public string BankTranTime { get { return BankTransactionTime.ToString().Substring(0, 8); } }
        public decimal? TotalAmount { get; set; }
        public string PayMode { get; set; }
    }
}