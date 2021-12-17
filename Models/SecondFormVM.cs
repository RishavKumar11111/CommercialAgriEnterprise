using System;
using System.Linq;

namespace CommercialAgriEnterprise.Models
{
    public class SecondFormVM
    {
        public string DepartmentID { get; set; }
        public string Department { get; set; }
        public string ProjectTypeID { get; set; }
        public string ProjectType { get { return new CommercialAgriEnterpriseEntities().ProjectTypes.Where(z => z.Code == ProjectTypeID).Select(x => x.Name).FirstOrDefault(); } }
        public string ProductsToBeProduced { get; set; }
        public decimal? Capacity { get; set; }
        public string rbIsCISAvailed { get; set; }
        public decimal OwnContribution { get; set; }
        public string FinanceType { get; set; }
        public decimal? BankLoan { get; set; }
        public decimal? TotalCost { get; set; }
        public string CapacityUnit { get; set; }
        public string Cisavailed { get; set; }
        public string approxCost { get; set; }
        public string BankCode { get; set; }
        public string BankName { get; set; }
        public string BranchCode { get; set; }
        public string BranchName { get; set; }
        public string CISProjectTypeCode { get; set; }
        public string CISProjectType { get { return new CommercialAgriEnterpriseEntities().ProjectTypes.Where(z => z.Code == CISProjectTypeCode).Select(x => x.Name).FirstOrDefault(); } }
        public string CISLocation { get; set; }
        public string CISYear { get; set; }
        public string CISFinanceType { get; set; }
        public decimal? CISAmount { get; set; }
        public byte[] CISBankCertificate { get; set; }
        public string CISBCC { get { if (CISBankCertificate != null) { return Convert.ToBase64String(CISBankCertificate); } else { return null; } } }
        public byte[] BankConsentLetter { get; set; }
        public string BCL { get { if (BankConsentLetter != null) { return "Yes"; } else { return "No"; } } }
    }
}