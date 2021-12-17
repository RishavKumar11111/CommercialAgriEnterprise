using System;
using System.Collections.Generic;
using System.Linq;

namespace CommercialAgriEnterprise.Models
{
    public class DeptWiseAdminReportVM
    {
        public List<BindDept> GetAllDist { get { var k = new CommercialAgriEnterpriseEntities().BeneficiaryDetails.Select(g => new BindDept { DepartmentCode = g.BeneficiaryProjectDetail.DepartmentCode }).Distinct().ToList(); return k; } }
    }

    public class BindDept
    {
        public string DepartmentCode { get; set; }
        public string DepartmentName { get { if (DepartmentCode == "04") { return "ARD"; } else if (DepartmentCode == "01") { return "AGR"; } else if (DepartmentCode == "02") { return "HOR"; } else { return "FIS"; } } }
        public List<GetFarmerList> GetFarmerDetailsDeptwise { get { return new CommercialAgriEnterpriseEntities().BeneficiaryDetails.Where(g => g.ReferenceNo.Substring(4, 3) == DepartmentName).Select(g => new GetFarmerList { ReferenceNo = g.ReferenceNo, RegdStatus = g.RegistrationStatus, DPRStatus = g.DPRStatu.Status, PaymentStatus = g.PaymentStatus, FarmerID = g.FarmerID, BLOStatus = g.BLORecord.BLOStatus, DNOStatus = g.DNORecord.DNOStatus, DDNOStatus = g.DDARecord.IntegratedFarmingStatus, CollectorStatus = g.CollectorRecord.CollectorStatus, DDAStatus = g.DDARecord.DDAStatus }).ToList(); } }
    }
}