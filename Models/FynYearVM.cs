using System.Collections.Generic;
using System.Linq;

namespace CommercialAgriEnterprise.Models
{
    public class FynYearVM
    {
        public FynYearVM(string usernm)
        {
            _unm = usernm;
        }

        public string _unm { get; set; }

        public List<FynYr> FYR
        {
            get
            {
                List<FynYr> yr = new List<FynYr>();
                foreach (var k in new CommercialAgriEnterpriseEntities().BLORecords.Where(a => a.BLOUserName == _unm).Select(a => a.FinancialYear).Distinct().ToList())
                {
                    FynYr obj = new FynYr();
                    obj.fyr = k;
                    yr.Add(obj);
                }
                return yr;
            }
        }
    }

    public class FynYr
    {
        public string _unm { get; set; }
        public string fyr { get; set; }
    }
}