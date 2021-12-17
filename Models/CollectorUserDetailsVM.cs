using System;
using System.Collections.Generic;
using System.Linq;

namespace CommercialAgriEnterprise.Models
{
    public class CollectorUserDetailsVM
    {
        public CollectorUserDetailsVM(string _DistrictName, string dCode)
        {
            DistrictName = _DistrictName;
            DCode = dCode;
        }
        private string DistrictName { get; set; }
        public string DCode { get; set; }
        public List<_CollectorDetail> UserDetails { get { return new CommercialAgriEnterpriseEntities().BeneficiaryDetails.Where(z => (z.DNORecord.ReferenceNo == z.CollectorRecord.ReferenceNo || z.DDARecord.ReferenceNo == z.CollectorRecord.ReferenceNo) && (z.BLORecord.BLOStatus == 1 || z.BLORecord.BLOStatus == 2) && (z.DNORecord.DNOStatus == 1 || z.DNORecord.DNOStatus == 2 || z.DDARecord.IntegratedFarmingStatus == 1 || z.DDARecord.IntegratedFarmingStatus == 2) && z.CollectorRecord.CollectorStatus == 0 && z.RegistrationStatus == "completed" && z.DPRStatu.Status == 1 && z.PaymentStatus == "Success" && z.NICDistrictCode == DCode).Select(x => new _CollectorDetail { ReferenceNo = x.ReferenceNo, DepartmentCode = x.BeneficiaryProjectDetail.DepartmentCode, DepartmentName = x.BeneficiaryProjectDetail.Department.Name, ProjectTypeCode = x.BeneficiaryProjectDetail.ProjectTypeCode, ProjectTypeName = x.BeneficiaryProjectDetail.ProjectType.Name, FarmerID = x.FarmerID, BStatus = x.BLORecord.BLOStatus, DStatus = x.DNORecord.DNOStatus, DAStatus = x.DDARecord.IntegratedFarmingStatus }).ToList(); } }
    }
    public class _CollectorDetail
    {
        public string ReferenceNo { get; set; }
        public string FarmerID { get; set; }
        public string DepartmentCode { get; set; }
        public string DepartmentName { get; set; }
        public string ProjectTypeCode { get; set; }
        public string ProjectTypeName { get; set; }
        public string FarmerName { get { return new FARMERDBEntities().M_FARMER_REGISTRATION.Where(g => g.NICFARMERID == FarmerID).Select(g => g.VCHFARMERNAME).FirstOrDefault(); } }
        public int? BStatus { get; set; }
        public int? DStatus { get; set; }
        public int? DAStatus { get; set; }
        public string BLOStatus { get { if (BStatus == 2) { return "Not Feasible"; } else if (BStatus == 1) { return "Feasible"; } else { return "Pending"; } } }
        public string DNOStatus { get { if (DStatus == 2) { return "Not Recommended"; } else if (DAStatus == 2) { return "Not Recommended"; } else if (DStatus == 1) { return "Recommended"; } else if (DAStatus == 1) { return "Recommended"; } else { return "Pending"; } } }
    }
}