using System;
using System.Collections.Generic;
using System.Linq;

namespace CommercialAgriEnterprise.Models
{
    public class BankBAL
    {
        CommercialAgriEnterpriseEntities orm = new CommercialAgriEnterpriseEntities();
        FARMERDBEntities orm1 = new FARMERDBEntities();

        public List<BankMISDetail> GetMisDetail(string bankname)
        {
            var k = orm.BankMappings.Where(a => a.BankUserID == bankname).Select(a => a.BankCode).FirstOrDefault();
            return orm.BeneficiaryProjectDetails.Where(z => z.BankCode == k).Select(z => new BankMISDetail { ReferenceNo = z.ReferenceNo, FarmerID = z.BeneficiaryDetail.FarmerID, BankLoan = z.BankLoan, BrCode = z.BranchCode, BCode = z.BankCode, BankLoanStatus = z.BeneficiaryDetail.BankLoanStatu.Status }).ToList();
        }
    }

    public class BankMISDetail
    {
        public string FarmerID { get; set; }
        public FarmerDetail FDetail { get { return new FARMERDBEntities().M_FARMER_REGISTRATION.Where(x => x.NICFARMERID == FarmerID).Select(x => new FarmerDetail { FaremerName = x.VCHFARMERNAME, FatherName = x.VCHFATHERNAME }).FirstOrDefault(); } }
        public int BankCode { get { return Convert.ToInt32(BCode); } }
        public string BCode { get; set; }
        public string BankName { get { return new FARMERDBEntities().M_PDS_FARMERBANK.Where(z => z.intId == BankCode).Select(z => z.vchBankName).FirstOrDefault(); } }
        public int BranchCode { get { return Convert.ToInt32(BrCode); } }
        public string BrCode { get; set; }
        public string BranchName { get { return new FARMERDBEntities().M_PDS_BANKBRANCH.Where(z => z.intBranchId == BranchCode).Select(z => z.vchBranchName).FirstOrDefault(); } }
        public string ReferenceNo { get; set; }
        public decimal? BankLoan { get; set; }
        public string BankLoanStatus { get; set; }

    }

    public class FarmerDetail
    {
        public string FaremerName { get; set; }
        public string FatherName { get; set; }
    }
}