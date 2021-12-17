using System;
using System.Collections.Generic;
using System.Linq;

namespace CommercialAgriEnterprise.Models
{
    public class BLODetailsEntryVM
    {
        public BLODetailsEntryVM(string DistCode)
        {
            _DistCode = Convert.ToInt32(DistCode);
        }
        private int _DistCode { get; set; }
        public List<BindBlock> getAlldist { get { return new CommercialAgriEnterpriseEntities().BLODetailEntries.Where(g => g.DistrictCode == g.LGDDistrict.DistrictCode && g.DistrictCode == _DistCode).Select(g => new BindBlock { DistCode = g.DistrictCode, DeptCode = g.DepartmentCode }).Distinct().ToList(); } }
    }

    public class BindBlock
    {
        public string DeptCode { get; set; }
        public int? DistCode { get; set; }
        public int DistrictCode { get { return Convert.ToInt32(DistCode); } }
        public string DistName { get { return new CommercialAgriEnterpriseEntities().LGDDistricts.Where(a => a.DistrictCode == DistrictCode).Select(a => a.DistrictName.Trim()).FirstOrDefault(); } }
        public string DepartmentName { get { return new CommercialAgriEnterpriseEntities().Departments.Where(a => a.Code == DeptCode).Select(a => a.Name.Trim()).FirstOrDefault(); } }
        public List<getBlockList> getAllBlockList { get { return new CommercialAgriEnterpriseEntities().BLODetailEntries.Where(g => g.DistrictCode == DistrictCode && g.DepartmentCode == DeptCode).Select(g => new getBlockList { BlockCode = g.BlockCode, BloName = g.Name, MobileNumber = g.MobileNo }).ToList(); } }
    }

    public class getBlockList
    {
        public int BlockCode { get; set; }
        public string BlockName { get { return new CommercialAgriEnterpriseEntities().LGDBlocks.Where(z => z.BlockCode == BlockCode).Select(z => z.BlockName).FirstOrDefault(); } }
        public string BloName { get; set; }
        public string MobileNumber { get; set; }
    }
}