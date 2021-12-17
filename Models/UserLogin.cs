using System;
using System.Data.Entity;
using System.Linq;

namespace CommercialAgriEnterprise.Models
{
    public static class UserLogin
    {
        public static UserDetailsDNO DNOLogin(string _UserID)
        {
            return new CommercialAgriEnterpriseEntities().DNODistrictMappings.Where(a => a.UserID == _UserID).Select(a => new UserDetailsDNO { DistrictID = a.DistrictCode, DistrictName = a.LGDDistrict.PDSDistrictName, Department = a.Department.Name, DepartmentCode = a.Department.Code }).FirstOrDefault();
        }

        public static int dnonotification(string _UserID, string deptcode, string DCode)
        {
            var k = new CommercialAgriEnterpriseEntities().BeneficiaryDetails.Where(a => (a.BeneficiaryProjectDetail.DepartmentCode == deptcode || a.DDARecord.IntegratedFarmingStatus == 0) && (a.BLORecord.ReferenceNo == a.DNORecord.ReferenceNo || a.BLORecord.ReferenceNo == a.DDARecord.ReferenceNo) && (a.BLORecord.BLOStatus == 1 || a.BLORecord.BLOStatus == 2) && a.RegistrationStatus == "completed" && a.DPRStatu.Status == 1 && a.PaymentStatus == "Success" && (a.DNORecord.DNOStatus == 0 || a.DDARecord.DDAStatus == 0) && DbFunctions.AddDays(a.BLORecord.BLODate, 15) >= DateTime.Now && a.NICDistrictCode == DCode).Count();
            return k;
        }

        public static UserDetailsBLO BLOLogin(string _UserID)
        {
            return new CommercialAgriEnterpriseEntities().BLOBlockMappings.Where(a => a.UserID == _UserID).Select(a => new UserDetailsBLO { BlockID = a.BlockCode, BlockName = a.LGDBlock.BlockName, Department = a.Department.Name, DepartmentCode = a.DeptCode }).FirstOrDefault();
        }

        public static int blonotification(string _UserID, string deptcode)
        {
            var k = new CommercialAgriEnterpriseEntities().Payments.Where(a => a.BeneficiaryDetail.RegistrationStatus == "completed" && a.BeneficiaryDetail.DPRStatu.Status == 1 && a.BeneficiaryDetail.BLORecord.BLOStatus == 0 && a.BeneficiaryDetail.PaymentStatus == "Success" && a.TransactionStatus == "Success" && (a.BeneficiaryDetail.IntegratedProjectLog.NewDeptCode == deptcode || a.BeneficiaryDetail.BeneficiaryProjectDetail.DepartmentCode == deptcode) && a.BeneficiaryDetail.BLORecord.BLOUserName == _UserID && DbFunctions.AddDays(a.TransactionDate, 15) >= DateTime.Now).Count();
            return k;
        }

        public static UserDetailsCollector CollectorLogin(string _UserID)
        {
            return new CommercialAgriEnterpriseEntities().CollectorDistrictMappings.Where(z => z.UserID == _UserID).Select(x => new UserDetailsCollector { DistrictCode = x.DistrictCode, DistrictName = x.LGDDistrict.PDSDistrictName }).FirstOrDefault();
        }

        public static int collectornotification(string _UserID, string DCode)
        {
            var k = new CommercialAgriEnterpriseEntities().BeneficiaryDetails.Where(z => (z.DNORecord.ReferenceNo == z.CollectorRecord.ReferenceNo || z.DDARecord.ReferenceNo == z.CollectorRecord.ReferenceNo) && (z.BLORecord.BLOStatus == 1 || z.BLORecord.BLOStatus == 2) && (z.DNORecord.DNOStatus == 1 || z.DNORecord.DNOStatus == 2 || z.DDARecord.IntegratedFarmingStatus == 1 || z.DDARecord.IntegratedFarmingStatus == 2) && z.CollectorRecord.CollectorStatus == 0 && z.RegistrationStatus == "completed" && z.DPRStatu.Status == 1 && z.PaymentStatus == "Success" && z.NICDistrictCode == DCode).Count();
            return k;
        }
    }

    public class UserDetailsDNO
    {
        public int DistrictID { get; set; }
        public string DistrictName { get; set; }
        public string Department { get; set; }
        public string DepartmentCode { get; set; }
    }

    public class UserDetailsBLO
    {
        public int BlockID { get; set; }
        public string BlockName { get; set; }
        public string Department { get; set; }
        public string DepartmentCode { get; set; }
    }

    public class UserDetailsCollector
    {
        public int DistrictCode { get; set; }
        public string DistrictName { get; set; }
    }
}