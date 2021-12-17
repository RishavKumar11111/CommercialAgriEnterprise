using System.Collections.Generic;
using System.Linq;

namespace CommercialAgriEnterprise.Models
{
    public class GetdnoFYRVM
    {
        public GetdnoFYRVM(string usernm)
        {
            _un = usernm;
        }

        public string _un { get; set; }

        public List<FYR> getfyr
        {
            get
            {
                List<FYR> yr = new List<FYR>();
                foreach (var k in new CommercialAgriEnterpriseEntities().DNORecords.Where(a => a.DNOUserName == _un).Select(a => a.FinancialYear).Distinct().ToList())
                {
                    FYR obj = new FYR();
                    obj.fyr = k;
                    yr.Add(obj);
                }
                return yr;
            }
        }
    }

    public class FYR
    {
        public string _un { get; set; }
        public string fyr { get; set; }
    }
}