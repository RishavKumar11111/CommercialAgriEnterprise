using System;
using System.Collections.Generic;
using System.Linq;

namespace CommercialAgriEnterprise.Models
{
    public class CollectorDDAApprovalListVM
    {
        public CollectorDDAApprovalListVM(string _DistrictName, string dCode, int _LotNo)
        {
            DistrictName = _DistrictName;
            DCode = dCode;
            LotNo = _LotNo;
        }
        private string DistrictName { get; set; }
        public string DCode { get; set; }
        private int LotNo { get; set; }
        public List<DLSCDDAVM> GetRecordsByLotNo { get { return new CommercialAgriEnterpriseEntities().BeneficiaryDetails.Where(z => z.NICDistrictCode == DCode && z.CollectorRecord.CollectorStatus == 1 && z.CollectorRecord.LotNo == LotNo).Select(x => new DLSCDDAVM { ReferenceNo = x.ReferenceNo, DepartmentCode = x.BeneficiaryProjectDetail.DepartmentCode, DepartmentName = x.BeneficiaryProjectDetail.Department.Name, ProjectTypeCode = x.BeneficiaryProjectDetail.ProjectTypeCode, ProjectTypeName = x.BeneficiaryProjectDetail.ProjectType.Name, FarmerID = x.FarmerID, BStatus = x.BLORecord.BLOStatus, DStatus = x.DNORecord.DNOStatus, DateOfMeeting = x.CollectorRecord.DateOfMeeting, TimeOfMeeting = x.CollectorRecord.TimeOfMeeting, UpdatedDOM = x.CollectorRecord.UpdatedDOM, UpdatedTOM = x.CollectorRecord.UpdatedTOM, StatusDDA = x.DDARecord.DDAStatus, CollectorUpdateStatus = x.CollectorRecord.CollectorUpdateStatus, DAStatus = x.DDARecord.IntegratedFarmingStatus }).ToList(); } }
    }
    public class DLSCDDAVM
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
        public DateTime? DateOfMeeting { get; set; }
        public DateTime? UpdatedDOM { get; set; }
        public string UDate { get { if (UpdatedDOM != null) { return Convert.ToDateTime(UpdatedDOM).ToString("dd-MM-yyyy").Substring(0, 10); } else { return Convert.ToDateTime(DateOfMeeting).ToString("dd-MM-yyyy").Substring(0, 10); } } }
        public TimeSpan? TimeOfMeeting { get; set; }
        public TimeSpan? UpdatedTOM { get; set; }
        public string UTime { get { if (UpdatedTOM != null) { return UpdatedTOM.ToString().Substring(0, 5); } else { return TimeOfMeeting.ToString().Substring(0, 5); } } }
        public int? StatusDDA { get; set; }
        public string DDAStatus { get { if (StatusDDA == 2) { return "Rejected"; } else if (StatusDDA == 1) { return "Approved"; } else { return "Pending"; } } }
        public int? CollectorUpdateStatus { get; set; }
    }
}