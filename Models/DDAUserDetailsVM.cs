using System;
using System.Collections.Generic;
using System.Linq;

namespace CommercialAgriEnterprise.Models
{
    public class DDAUserDetailsVM
    {
        public DDAUserDetailsVM(string _DistrictName, string dCode)
        {
            DistrictName = _DistrictName;
            DCode = dCode;
        }
        private string DistrictName { get; set; }
        private string DCode { get; set; }
        public List<_DDADetail> userDetails { get { return new CommercialAgriEnterpriseEntities().BeneficiaryDetails.Where(z => (z.CollectorRecord.ReferenceNo == z.DDARecord.ReferenceNo) && z.NICDistrictCode == DCode && (z.BLORecord.BLOStatus == 1 || z.BLORecord.BLOStatus == 2) && (z.DNORecord.DNOStatus == 1 || z.DNORecord.DNOStatus == 2 || z.DDARecord.IntegratedFarmingStatus == 1 || z.DDARecord.IntegratedFarmingStatus == 2) && z.CollectorRecord.CollectorStatus == 1 && (z.DDARecord.DDAStatus == 0 || z.DDARecord.DDAStatus == 1 || z.DDARecord.DDAStatus == 2) && z.RegistrationStatus == "completed" && z.DPRStatu.Status == 1 && z.PaymentStatus == "Success").Select(x => new _DDADetail { ReferenceNo = x.ReferenceNo, DepartmentCode = x.BeneficiaryProjectDetail.DepartmentCode, DepartmentName = x.BeneficiaryProjectDetail.Department.Name, ProjectTypeCode = x.BeneficiaryProjectDetail.ProjectTypeCode, ProjectTypeName = x.BeneficiaryProjectDetail.ProjectType.Name, FarmerID = x.FarmerID, LotNo = x.CollectorRecord.LotNo, BStatus = x.BLORecord.BLOStatus, DStatus = x.DNORecord.DNOStatus, DAStatus = x.DDARecord.IntegratedFarmingStatus, DateOfMeeting = x.CollectorRecord.DateOfMeeting, UpdatedDOM = x.CollectorRecord.UpdatedDOM, TimeOfMeeting = x.CollectorRecord.TimeOfMeeting, UpdatedTOM = x.CollectorRecord.UpdatedTOM, StatusDDA = x.DDARecord.DDAStatus }).ToList(); } }
    }

    public class _DDADetail
    {
        public string ReferenceNo { get; set; }
        public string FarmerID { get; set; }
        public string DepartmentCode { get; set; }
        public string DepartmentName { get; set; }
        public string ProjectTypeCode { get; set; }
        public string ProjectTypeName { get; set; }
        public int? LotNo { get; set; }
        public DateTime? DateOfMeeting { get; set; }
        public DateTime? UpdatedDOM { get; set; }
        public string UDate { get { if (UpdatedDOM != null) { return Convert.ToDateTime(UpdatedDOM).ToString("dd-MM-yyyy").Substring(0, 10); } else { return Convert.ToDateTime(DateOfMeeting).ToString("dd-MM-yyyy").Substring(0, 10); } } }
        public TimeSpan? TimeOfMeeting { get; set; }
        public TimeSpan? UpdatedTOM { get; set; }
        public string UTime { get { if (UpdatedTOM != null) { return UpdatedTOM.ToString().Substring(0, 5); } else { return TimeOfMeeting.ToString().Substring(0, 5); } } }
        public string FarmerName { get { return new FARMERDBEntities().M_FARMER_REGISTRATION.Where(g => g.NICFARMERID == FarmerID).Select(g => g.VCHFARMERNAME).FirstOrDefault(); } }
        public int? BStatus { get; set; }
        public int? DStatus { get; set; }
        public int? DAStatus { get; set; }
        public string BLOStatus { get { if (BStatus == 2) { return "Not Feasible"; } else if (BStatus == 1) { return "Feasible"; } else { return "Pending"; } } }
        public string DNOStatus { get { if (DStatus == 2) { return "Not Recommended"; } else if (DAStatus == 2) { return "Not Recommended"; } else if (DStatus == 1) { return "Recommended"; } else if (DAStatus == 1) { return "Recommended"; } else { return "Pending"; } } }
        public int? StatusDDA { get; set; }
        public string DDAStatus { get { if (StatusDDA == 2) { return "Rejected"; } else if (StatusDDA == 1) { return "Approved"; } else { return "Pending"; } } }
    }
}