using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CommercialAgriEnterprise.Models
{
    public class BindAllDistTargetWiseVM
    {
        public BindAllDistTargetWiseVM(string projecttypecode)
        {
            Pcode = projecttypecode;
            
        }
        public string Pcode;
        //public string FYEAR;
        public List<BindAllDist> GetDist { get { return new CommercialAgriEnterpriseEntities().LGDDistricts.Select(g => new BindAllDist { DistCode = g.DistrictCode, DistName = g.DistrictName, ProjectCode = Pcode  }).Distinct().ToList(); } }
    }
    public class BindAllDist
    {
        public int DistCode { get; set; }
        public string DistName { get; set; }
        public string ProjectCode { get; set; }
        //public string Fyear { get; set; }
        public GetTargetList GetTargetList { get { return new CommercialAgriEnterpriseEntities().Targets.Where(g => g.ProjectTypeCode == ProjectCode && g.DistrictCode == DistCode ).Select(g => new GetTargetList { DistCode = DistCode, ProjectTypeCode = ProjectCode, GEN = g.GeneralValue, SC = g.SCValue, ST = g.STValue }).FirstOrDefault(); } }
    }

    public class GetTargetList
    {
        public int DistCode { get; set; }
        public string ProjectTypeCode { get; set; }
        public int GEN { get; set; }
        public int SC { get; set; }
        public int ST { get; set; }
    }
}