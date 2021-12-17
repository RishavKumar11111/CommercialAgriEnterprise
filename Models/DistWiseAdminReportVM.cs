using System;
using System.Collections.Generic;
using System.Linq;

namespace CommercialAgriEnterprise.Models
{
    public class DistWiseAdminReportVM
    {
        public List<BindAllDistrict> DistWiseReport { get { var k = new CommercialAgriEnterpriseEntities().BeneficiaryDetails.Select(g => new BindAllDistrict { DistrictCode = g.NICDistrictCode }).Distinct().ToList(); return k; } }
    }
    public class BindAllDistrict
    {
        public string DistrictCode { get; set; }
        public int DistCode { get { return Convert.ToInt32(DistrictCode); } }
        public string DistName { get { return new CommercialAgriEnterpriseEntities().LGDDistricts.Where(a => a.DistrictCode == DistCode).Select(a => a.PDSDistrictName).FirstOrDefault(); } }
        public List<GetFarmerList> GetFarmerDetails { get { return new CommercialAgriEnterpriseEntities().BeneficiaryDetails.Where(g => g.NICDistrictCode == DistrictCode).Select(g => new GetFarmerList { ReferenceNo = g.ReferenceNo, RegdStatus = g.RegistrationStatus, DPRStatus = g.DPRStatu.Status, PaymentStatus = g.PaymentStatus, FarmerID = g.FarmerID, BLOStatus = g.BLORecord.BLOStatus, DNOStatus = g.DNORecord.DNOStatus, DDNOStatus = g.DDARecord.IntegratedFarmingStatus, CollectorStatus = g.CollectorRecord.CollectorStatus, DDAStatus = g.DDARecord.DDAStatus }).ToList(); } }
    }

    public class GetFarmerList
    {
        public string ReferenceNo { get; set; }
        public string RegdStatus { get; set; }
        public string RegistrationStatus { get { if (RegdStatus == "completed") { return "Success"; } else { return "Pending"; } } }
        public int? DPRStatus { get; set; }
        public string StatusDPR { get { if (DPRStatus == 1) { return "Success"; } else { return "Pending"; } } }
        public string PaymentStatus { get; set; }
        public string StatusPayment { get { if (PaymentStatus == "Success") { return "Success"; } else { return "Pending"; } } }
        public int? BLOStatus { get; set; }
        public string StatusBLO { get { if (BLOStatus == 1) { return "Feasible"; } else if (BLOStatus == 2) { return "Not Feasible"; } else { return "Pending"; } } }
        public int? DDNOStatus { get; set; }
        public int? DNOStatus { get; set; }
        public string StatusDNO { get { if (DNOStatus == 1) { return "Recommended"; } else if (DDNOStatus == 1) { return "Recommended"; } else if (DNOStatus == 2) { return "Not Recommended"; } else if (DDNOStatus == 2) { return "Not Recommended"; } else { return "Pending"; } } }
        public int? CollectorStatus { get; set; }
        public string StatusCollector { get { if (CollectorStatus == 1) { return "Meeting Date Fixed"; } else { return "Meeting Date not Fixed"; } } }
        public int? DDAStatus { get; set; }
        public string StatusDDA { get { if (DDAStatus == 1) { return "Approved"; } else if (DDAStatus == 2) { return "Rejected"; } else { return "Pending"; } } }
        public int? GoAheadStatus { get { return new CommercialAgriEnterpriseEntities().GoAheads.Where(z => z.ReferenceNo == ReferenceNo).Select(x => x.GoAheadStatus).FirstOrDefault(); } }
        public string StatusGoAhead { get { if (GoAheadStatus == 1) { return "Generated"; } else { return "Not Generated"; } } }
        public string FarmerID { get; set; }
        public M_FARMER_REGISTRATION FDetails { get { return new FARMERDBEntities().M_FARMER_REGISTRATION.Where(z => z.NICFARMERID == FarmerID).FirstOrDefault(); } }
        public string FarmerName { get { return FDetails.VCHFARMERNAME; } }
        public string FatherName { get { return FDetails.VCHFATHERNAME; } }
    }
}