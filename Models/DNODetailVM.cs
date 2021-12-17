using System;
using System.Linq;

namespace CommercialAgriEnterprise.Models
{
    public class DNODetailVM
    {
        public DNODetailVM(string _departmentCode, int _districtCode)
        {
            departmentCode = _departmentCode;
            districtCode = _districtCode;
        }
        private string departmentCode { get; set; }
        private int districtCode { get; set; }
        public DNODetailRecords GetDNODetails { get { return new CommercialAgriEnterpriseEntities().DNODetailEntries.Where(z => z.DepartmentCode == departmentCode && z.DistrictCode == districtCode).Select(x => new DNODetailRecords { Name = x.Name, MobileNo = x.MobileNo, Signature = x.Signature, DeptCode = departmentCode, DistCode = districtCode, DeptName = x.Department.Name, DistName = x.LGDDistrict.DistrictName }).FirstOrDefault(); } }
    }
    public class DNODetailRecords
    {
        public string Name { get; set; }
        public string MobileNo { get; set; }
        public byte[] Signature { get; set; }
        public string Sign { get { return Convert.ToBase64String(Signature); } }
        public string DeptCode { get; set; }
        public string DeptName { get; set; }
        public int DistCode { get; set; }
        public string DistName { get; set; }
    }
}