using System;
using System.Collections.Generic;
using System.Linq;

namespace CommercialAgriEnterprise.Models
{
    public class UnlockUserALL
    {
        ApplicationDbContext x = new ApplicationDbContext();
        public List<_BloUserlock> GetlockuserBLO { get { return x.Users.Where(a => (a.UserName.Substring(0, 3) == "aao" || a.UserName.Substring(0, 3) == "afo" || a.UserName.Substring(0, 3) == "aho" || a.UserName.Substring(0, 3) == "bvo") && a.AccessFailedCount == 3).Select(g => new _BloUserlock { UserName = g.UserName, EmailID = g.Email }).DefaultIfEmpty().ToList(); } }

        public List<_Dnoserlock> GetlockuserDNO { get { return x.Users.Where(a => a.AccessFailedCount == 3 && a.UserName.Substring(0, 3) == ("dno")).Select(g => new _Dnoserlock { UserName = g.UserName, EmailID = g.Email }).ToList(); } }

        public List<_CollectorLock> GetLockuserCOLLECTOR { get { return x.Users.Where(z => z.AccessFailedCount == 3 && z.UserName.Substring(0, 9) == "collector").Select(z => new _CollectorLock { Username = z.UserName, EmailID = z.Email }).ToList(); } }
    }

    public class _BloUserlock
    {
        public string UserName { get; set; }
        public string EmailID { get; set; }
        public string uid { get { return UserName.Substring(4, 4); } }
        public int? bloUserid { get { return Convert.ToInt32(uid); } }
        public string BlockName { get { return new CommercialAgriEnterpriseEntities().LGDBlocks.Where(a => a.BlockCode == bloUserid).Select(g => g.BlockName).FirstOrDefault(); } }
        public string BloUsernm { get { return (UserName.Substring(0, 3) + "  " + BlockName).ToUpper(); } }
    }

    public class _Dnoserlock
    {
        public string UserName { get; set; }
        public string EmailID { get; set; }
        public string dept { get { if (UserName.Substring(4, 3) == "fis") { return "FISHERY"; } else if (UserName.Substring(4, 3) == "ard") { return "ANIMAL HUSBANDRY"; } else if (UserName.Substring(4, 3) == "hor") { return "HORTICULTURE"; } else { return "AGRICULTURE"; } } }
        public string[] Distnm { get { return UserName.Split('_'); } }
        public string DnoUsernm { get { return (UserName.Substring(0, 3) + "  " + dept + "  " + Distnm[2]).ToUpper(); } }
    }

    public class _CollectorLock
    {
        public string Username { get; set; }
        public string EmailID { get; set; }
    }
}