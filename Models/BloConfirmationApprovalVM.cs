using System;
using System.Collections.Generic;
using System.Linq;

namespace CommercialAgriEnterprise.Models
{
    public class BloConfirmationApprovalVM
    {
        public BloConfirmationApprovalVM(string startdate, string enddate, string username, string fyr)
        {
            DateTime strtdt = Convert.ToDateTime(startdate.Substring(4, 11));
            string startingdt = strtdt.ToString("yyyy/MM/dd");
            a = Convert.ToDateTime(startingdt);
            DateTime enddt = Convert.ToDateTime(enddate.Substring(4, 11));
            string endingdt = Convert.ToDateTime(enddt).ToString("yyyy/MM/dd");
            b = Convert.ToDateTime(endingdt);
            Username = username;
            _fyr = fyr;
        }

        private DateTime a { get; set; }
        private DateTime b { get; set; }
        private string Username { get; set; }
        private string _fyr { get; set; }

        public List<_bloapprovelist> getapprovelist
        {
            get
            {
                var t = new CommercialAgriEnterpriseEntities().BLORecords.Where(z => z.BLOUserName == Username && z.BLOStatus == 1 && z.BLODate >= a && z.BLODate <= b && z.BeneficiaryDetail.ReferenceNo == z.ReferenceNo).Select(z => new _bloapprovelist { refno = z.ReferenceNo, farmerid = z.BeneficiaryDetail.FarmerID }).ToList();
                var f = t;
                return new CommercialAgriEnterpriseEntities().BLORecords.Where(z => z.BLOUserName == Username && z.BLOStatus == 1 && z.BLODate >= a && z.BLODate <= b && z.BeneficiaryDetail.ReferenceNo == z.ReferenceNo && z.FinancialYear == _fyr).Select(z => new _bloapprovelist { refno = z.ReferenceNo, farmerid = z.BeneficiaryDetail.FarmerID }).ToList();
            }
        }
    }

    public class _bloapprovelist
    {
        public string refno { get; set; }
        public string farmerid { get; set; }
        public string farmername { get { return new FARMERDBEntities().M_FARMER_REGISTRATION.Where(g => g.NICFARMERID == farmerid).Select(g => g.VCHFARMERNAME).FirstOrDefault(); } }
    }
}