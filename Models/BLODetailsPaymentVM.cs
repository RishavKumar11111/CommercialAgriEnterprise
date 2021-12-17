using System;
using System.Collections.Generic;
using System.Linq;

namespace CommercialAgriEnterprise.Models
{
    public class BLODetailsPaymentVM
    {
        public List<DistWiseRecord> GetRecords { get { var k = new CommercialAgriEnterpriseEntities().BeneficiaryDetails.Where(z => z.PaymentStatus == "Success").Select(x => new DistWiseRecord { DistrictCode = x.NICDistrictCode }).Distinct().ToList(); return k; } }
    }

    public class DistWiseRecord
    {
        public string DistrictCode { get; set; }
        public int DistCode { get { return Convert.ToInt32(DistrictCode); } }
        public string DistrictName { get { return new CommercialAgriEnterpriseEntities().LGDDistricts.Where(a => a.DistrictCode == DistCode).Select(a => a.PDSDistrictName).FirstOrDefault(); } }
        public List<BeneficiaryBLODetailStatus> GetBeneficiaryBLO { get { return new CommercialAgriEnterpriseEntities().BeneficiaryDetails.Where(z => z.NICDistrictCode == DistrictCode && z.PaymentStatus == "Success").Select(x => new BeneficiaryBLODetailStatus { ReferenceNo = x.ReferenceNo, FarmerID = x.FarmerID, PaymentStatus = x.PaymentStatus, BLOStat = x.BLORecord.BLOStatus, BlockCode = x.NICBlockCode, DeptCode = x.BeneficiaryProjectDetail.DepartmentCode, DNOStat = x.DNORecord.DNOStatus, DCode = DistCode }).ToList(); } }
    }

    public class BeneficiaryBLODetailStatus
    {
        public string ReferenceNo { get; set; }
        public string FarmerID { get; set; }
        public string FarmerName { get { return new FARMERDBEntities().M_FARMER_REGISTRATION.Where(z => z.NICFARMERID == FarmerID).Select(x => x.VCHFARMERNAME).FirstOrDefault(); } }
        public string PaymentStatus { get; set; }
        public DateTime PayDate { get { return new CommercialAgriEnterpriseEntities().Payments.Where(z => z.ReferenceNo == ReferenceNo && z.TransactionStatus == "Success").Select(x => x.TransactionDate).FirstOrDefault(); } }
        public string PaymentDate { get { return PayDate.ToString("dd-MM-yyyy"); } }
        public int? BLOStat { get; set; }
        public string BLOStatus { get { if (BLOStat == 1) { return "Feasible"; } else if (BLOStat == 2) { return "Not Feasible"; } else { return "Pending"; } } }
        public BLODetails BLODetails { get { return new CommercialAgriEnterpriseEntities().BLODetailEntries.Where(z => z.BlockCode == BLKCode && z.DepartmentCode == DeptCode).Select(x => new BLODetails { BLOName = x.Name, BLOMobNo = x.MobileNo }).FirstOrDefault(); } }
        public string DeptCode { get; set; }
        public string BlockCode { get; set; }
        public int BLKCode { get { return Convert.ToInt32(BlockCode); } }
        public string BlockName { get { return new CommercialAgriEnterpriseEntities().LGDBlocks.Where(z => z.BlockCode == BLKCode).Select(x => x.BlockName).FirstOrDefault(); } }
        public int? DNOStat { get; set; }
        public string DNOStatus { get { if (DNOStat == 1) { return "Recommended"; } else if (DNOStat == 2) { return "Not Recommended"; } else { return "Pending"; } } }
        public DNODetails DNODetails { get { return new CommercialAgriEnterpriseEntities().DNODetailEntries.Where(z => z.DistrictCode == DCode && z.DepartmentCode == DeptCode).Select(x => new DNODetails { DNOName = x.Name, DNOMobNo = x.MobileNo }).FirstOrDefault(); } }
        public int DCode { get; set; }
    }

    public class DNODetails
    {
        public string DNOName { get; set; }
        public string DNOMobNo { get; set; }
    }

    public class BLODetails
    {
        public string BLOName { get; set; }
        public string BLOMobNo { get; set; }
    }
}