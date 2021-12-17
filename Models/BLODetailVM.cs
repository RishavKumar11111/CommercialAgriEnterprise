using System;
using System.Collections.Generic;
using System.Linq;

namespace CommercialAgriEnterprise.Models
{
    public class BLODetailVM
    {
        public BLODetailVM(string _departmentCode, int _districtCode)
        {
            departmentCode = _departmentCode;
            districtCode = _districtCode;
        }
        private string departmentCode { get; set; }
        private int districtCode { get; set; }
        public List<BLODetailRecords> GetBLODetails { get { return new CommercialAgriEnterpriseEntities().BLODetailEntries.Where(z => z.DepartmentCode == departmentCode && z.DistrictCode == districtCode).Select(x => new BLODetailRecords { BlockCode = x.BlockCode, BlockName = x.LGDBlock.BlockName, Name = x.Name, MobileNo = x.MobileNo, EmailID = x.EmailID, Signature = x.Signature }).ToList(); } }
    }
    public class BLODetailRecords
    {
        public int BlockCode { get; set; }
        public string BlockName { get; set; }
        public string Name { get; set; }
        public string MobileNo { get; set; }
        public string EmailID { get; set; }
        public byte[] Signature { get; set; }
        public string Sign { get { return Convert.ToBase64String(Signature); } }
    }
}